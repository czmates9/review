create table [CZMST_SE]
(
    [CountEntries] INTEGER not null,
    [SOPNUMBE] TEXT not null,
    [ITEMNMBR] TEXT,
    [ITEMTYPE] TEXT not null default (''),
    [ITEMDESC] TEXT,
    [VNDDOCNM] TEXT,
    [VNDITNUM] TEXT,
    [ORD] INTEGER not null,
    [CZ_CarKod] TEXT not null,
    [LOCNCODE] TEXT,
    [QTYSHPPD] NUMERIC not null,
    [QTYPACK] NUMERIC not null,
    [CZ_DatVyr_Track] INTEGER not null,
    [CZ_DatVyr_Delka] INTEGER not null,
    [CZ_SerNum_Track] INTEGER not null,
    [CZ_SerNum_Delka] INTEGER not null,
    [CZ_SW_Track] INTEGER not null,
    [CZ_SW_Delka] INTEGER not null,
    [CZ_Doslo] INTEGER not null,
    [DEX_ROW_ID] INTEGER not null,
    [Note] TEXT not null default '',
    [TYPEPAL] TEXT,
    [QTYPAL] NUMERIC,
    [PRINTED] INTEGER default 0,
    [PRIORITY] INTEGER not null default 3,
    [SKL_ID] TEXT,
    [MJ] TEXT,
    [CZ_REZ1_TRACK] INTEGER,
    [CZ_REZ2_TRACK] INTEGER,
    [ITEMCODE] TEXT,
    [WEIGHT] NUMERIC,
	[CZ_Expirace_Track] INTEGER
);
create index CZCARKOD on [CZMST_SE] ([CZ_CarKod]);
create index ITEMNMBR on [CZMST_SE] ([ITEMNMBR]);
create index [Key] on [CZMST_SE] ([CountEntries],[SOPNUMBE],[ITEMNMBR],[ORD]);
create index VNDITNUM on [CZMST_SE] ([VNDITNUM]);

create table [CZMST_SE_SN]
(
    [CountEntries] INTEGER not null,
    [ITEMNMBR] TEXT,
    [SERLNMBR] TEXT not null,
    [DEX_ROW_ID] INTEGER not null,
    [QTY] NUMERIC default 1,
    [SOPNUMBE] TEXT,
    [ORD] INTEGER,
    [Expirace] TEXT NULL
);
create index IDX_ITEM_SN on [CZMST_SE_SN] ([ITEMNMBR],[SERLNMBR]);
create index IDX_ITEMNMBR on [CZMST_SE_SN] ([ITEMNMBR]);

create table [CZMST_SEH]
(
    [CountEntries] INTEGER CONSTRAINT CZMST_SEH_CountEntries_PK PRIMARY KEY,
    [GUID] uniqueidentifier
);

create table [CZMST_SI]
(
    [CountEntries] INTEGER not null,
    [SOPNUMBE] TEXT not null,
    [ITEMNMBR] TEXT,
    [ORD] INTEGER not null,
    [VNDDOCNM] TEXT,
    [VNDITNUM] TEXT,
    [CZ_CarKod] TEXT,
    [LOCNCODE] TEXT,
    [QTYSHPPD] NUMERIC not null,
    [QTYPACK] NUMERIC not null,
    [QTYSHPPDMJ] NUMERIC not null,
    [SERLTNUM] TEXT not null,
    [KOD_SW] TEXT,
    [DAT_VYROBY] TEXT,
    [REZ_1] TEXT,
    [ODBER_ID] TEXT,
    [DATEDONE] TEXT,
    [TIMEDONE] TEXT,
    [USER_ID] INTEGER not null,
    [DEX_ROW_ID] INTEGER,
    [guid] uniqueidentifier,
    [TYPEPAL] TEXT,
    [NMBRPAL] TEXT,
    [PRINTED] NUMERIC default 0,
    [REZ_2] TEXT default '',
    [INPUT_MODE] INTEGER not null,
    [ID_TERMINAL] INTEGER not null,
    [SKL_ID] TEXT,
    [MJ] TEXT,
    [ITEMCODE] TEXT,
    [WEIGHT] NUMERIC,
    [Expirace] TEXT NULL
);
create unique index IX_CZMST_SI on [CZMST_SI] ([guid]);
create index [Key_SI] on [CZMST_SI] ([SOPNUMBE],[ITEMNMBR],[ORD],[SERLTNUM]);

create table [CZMST_SIH]
(
    [CountEntries] INTEGER CONSTRAINT CZMST_SIH_CountEntries_PK PRIMARY KEY,
    [TISKARNA_NAME] TEXT,
    [PRAC_ID] TEXT
);

create table [CZMST_SI_BV](
	[CountEntries] INTEGER NOT NULL,
	[USER_ID] INTEGER NOT NULL,
	[NMBRPAL] TEXT NULL,
	[pal_WEIGHT] NUMERIC NULL,
	[pal_W] NUMERIC NULL,
	[pal_H] NUMERIC NULL,
	[pal_D] NUMERIC NULL
);


create table [Parametry]
(
    [ENABLE_LISTSNIM] NUMERIC not null,
    [CONFIG_LISTSNIM] NUMERIC not null,
    [CONFIG_POKRDOHLED] NUMERIC not null,
    [CONFIG_PTATSE_NEANO] NUMERIC not null,
    [CONFIG_KONT_UPL_POL] NUMERIC not null,
    [CONFIG_ZADAT_MN_POKAZDE] NUMERIC not null,
    [ENABLE_DOHLED_ODB] NUMERIC not null,
    [CONFIG_DOHLED_ODB] NUMERIC not null,
    [CONFIG_KONT_DOKONCENOSTI] NUMERIC not null,
    [CONFIG_KONT_DELKA] NUMERIC not null,
    [CONFIG_KONT_SN_CARKOD] NUMERIC not null,
    [CONFIG_DUPLIC_SN] NUMERIC not null,
    [CONFIG_NOVA_KARTA] NUMERIC not null,
    [CONFIG_SKRYT_MNOZSTVI] NUMERIC not null,
    [CONFIG_SNIMEJ_PRI_LISTU] NUMERIC not null,
    [CONFIG_KONT_NUL_DELKA] NUMERIC not null,
    [CONFIG_SNIM_LOCNCODE] NUMERIC not null,
    [CONFIG_MNOZSTVI_PREDVYPLNIT] NUMERIC not null default 0,
    [CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA] NUMERIC not null default 0,
    [CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI] NUMERIC not null default 0,
    [CONFIG_MNOZSTVI_ZADAVAT] NUMERIC not null default 1,
    [CONFIG_MNOZSTVI_SCANNEREM] NUMERIC default 0,
    [CONFIG_KONT_PREDLOHA_SN] NUMERIC,
    [CONFIG_LOCNCODE_OVERIT_SCANEREM] NUMERIC,
    [CONFIG_POUZIT_CISELNIK_ZBOZI] NUMERIC not null,
    [CONFIG_NAZEV_SLOUPCE_K_OVERENI_V_CISELNIKU] TEXT,
    [CONFIG_NAZEV_SLOUPCE_K_NAVRACENI_V_CISELNIKU] TEXT,
    [CONFIG_LOKACE_POVOLIT] NUMERIC,
    [CONFIG_LOKACE_TIMEOUT] INTEGER
);

