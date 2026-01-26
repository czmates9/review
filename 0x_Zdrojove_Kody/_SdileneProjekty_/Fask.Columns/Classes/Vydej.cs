using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Fask.Columns.CreateSQL;

namespace Fask.Columns
{
    public class Vydej
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

        private  string TableName_SE = "CZMST_SE";
        private  string TableName_SE_SN = "CZMST_SE_SN";
        private  string TableName_SEH = "CZMST_SEH";
        private  string TableName_SI = "CZMST_SI";
        private  string TableName_SIH = "CZMST_SIH";
        private  string TableName_Param = "Parametry";

        public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_SE = new Dictionary<string, ColumnType>();
        public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_SE_SN = new Dictionary<string, ColumnType>();
        public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_SEH = new Dictionary<string, ColumnType>();
        public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_SI = new Dictionary<string, ColumnType>();
        public  Dictionary<string, ColumnType> ColumnsInfo_CZMST_SIH = new Dictionary<string, ColumnType>();
        public  Dictionary<string, ColumnType> ColumnsInfo_Parametry = new Dictionary<string, ColumnType>();

        #region c'tor

		public Vydej(DS_Information ds)
        {

                #region z XML souboru

                #region SE

                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_SE = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SE.Trim());

                foreach (var dr in DT_SE)
                {
                    ColumnsInfo_CZMST_SE.AddIfNotExists(dr);
                }

                #endregion

                #region SE_SN

                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_SE_SN = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SE_SN.Trim());

                foreach (var dr in DT_SE_SN)
                {
                    ColumnsInfo_CZMST_SE_SN.AddIfNotExists(dr);
                }

                #endregion

                #region SEH

                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_SEH = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SEH.Trim());

                foreach (var dr in DT_SEH)
                {
                    ColumnsInfo_CZMST_SEH.AddIfNotExists(dr);
                }

                #endregion

                #region SI

                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_SI = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SI.Trim());

                foreach (var dr in DT_SI)
                {
                    ColumnsInfo_CZMST_SI.AddIfNotExists(dr);
                }

                #endregion

                #region SIH

                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_SIH = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_SIH.Trim());

                foreach (var dr in DT_SIH)
                {
                    ColumnsInfo_CZMST_SIH.AddIfNotExists(dr);
                }

                #endregion

                #region Parametry

                EnumerableRowCollection<Fask.Columns.CreateSQL.DS_Information.SchemaColumnsRow> DT_Parametry = ds.SchemaColumns.Where(x => x.TABLE_NAME.Trim() == TableName_Param.Trim());

                foreach (var dr in DT_Parametry)
                {
                    ColumnsInfo_Parametry.AddIfNotExists(dr);
                }

                #endregion

                #endregion
        }

        #endregion

    }
}

