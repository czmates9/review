create table [CZMST_PE]
(
    [CountEntries] INTEGER not null,
    [PONUMBER] TEXT not null,
    [ITEMNMBR] TEXT,
    [ITEMDESC] TEXT,
    [ORD] INTEGER not null,
    [VNDDOCNM] TEXT,
    [VNDITNUM] TEXT,
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
    [Nasnimano] NUMERIC not null default 0,
    [MJ] TEXT,
    [SKL_ID] TEXT,
    [WEIGHT] NUMERIC,
    [NMBRPAL] TEXT,
    [TYPEPAL] TEXT,
    [ITEMCODE] TEXT,
    [SERLTNUM] TEXT not null,
    [CZ_REZ1_TRACK] INTEGER default 0,
    [CZ_REZ2_TRACK] INTEGER default 0,
	[Realization_Start] TEXT NULL,
	[Realization_Stop] TEXT NULL,
	[CZ_Expirace_Track] INTEGER NOT NULL DEFAULT 0
);
create index CaroveKody on [CZMST_PE] ([VNDITNUM],[CZ_CarKod]);
create index CaroveKodyPalety on [CZMST_PE] ([VNDITNUM],[CZ_CarKod],[NMBRPAL]);
create index Nazev on [CZMST_PE] ([ITEMDESC]);

create table [CZMST_PE_SN]
(
    [CountEntries] INTEGER not null,
    [ITEMNMBR] TEXT not null,
    [SERLNMBR] TEXT not null,
    [DEX_ROW_ID] INTEGER not null,
	[Expirace] TEXT NULL
);

create table [CZMST_PEH]
(
    [CountEntries] TEXT not null,
    [GUID] uniqueidentifier,
	CONSTRAINT CZMST_PEH_PK primary key ([CountEntries])
);

create table [CZMST_PI]
(
    [CountEntries] INTEGER not null,
    [PONUMBER] TEXT not null,
    [ORD] INTEGER not null,
    [ITEMNMBR] TEXT,
    [VNDDOCNM] TEXT,
    [VNDITNUM] TEXT,
    [LOCNCODE] TEXT,
    [QTYSHPPD] NUMERIC not null,
    [QTYPACK] NUMERIC not null,
    [SERLTNUM] TEXT not null,
    [KOD_SW] TEXT,
    [DAT_VYROBY] TEXT,
    [DATEDONE] TEXT,
    [TIMEDONE] TEXT,
    [CZ_CarKod] TEXT,
    [REZ_1] TEXT,
    [REZ_2] TEXT,
    [USER_ID] INTEGER not null,
    [DEX_ROW_ID] INTEGER,
    [guid] uniqueidentifier not null,
    [INPUT_MODE] INTEGER not null,
    [ID_TERMINAL] INTEGER not null,
    [MJ] TEXT,
    [QTYSHPPDMJ] NUMERIC not null,
    [SKL_ID] TEXT,
    [WEIGHT] NUMERIC,
    [NMBRPAL] TEXT,
    [TYPEPAL] TEXT,
    [ITEMCODE] TEXT,
    [Expirace] TEXT NULL,
    [AttributeToSN] TEXT NULL
);
create index Vyhledavani1 on [CZMST_PI] ([PONUMBER],[ORD],[ITEMNMBR],[SERLTNUM]);
create index Vyhledavani2 on [CZMST_PI] ([ITEMNMBR],[SERLTNUM]);

create table [CZMST_PI_F]
(
    [IMG_NAME] TEXT not null,
    [GUID] uniqueidentifier not null,
    [DEX_ROW_ID] INTEGER identity not null
);

create table [CZMST_PIH]
(
    [CountEntries] INTEGER not null,
    [DATUMDOKLADU] TEXT
);

create table [Parametry]
(
    [CONFIG_DUPLIC_SN] NUMERIC not null,
    [CONFIG_KONT_DELKA] NUMERIC not null,
    [CONFIG_KONT_DOKONCENOSTI] NUMERIC not null,
    [CONFIG_KONT_NUL_DELKA] NUMERIC not null,
    [CONFIG_KONT_UPL_POL] NUMERIC not null,
    [CONFIG_LISTSNIM] NUMERIC not null,
    [CONFIG_NOVA_KARTA] NUMERIC not null,
    [CONFIG_POKRDOHLED] NUMERIC not null,
    [CONFIG_PRIM_KEY1] NUMERIC not null,
    [CONFIG_PTATSE_NEANO] NUMERIC not null,
    [CONFIG_SNIM_LOCNCODE] NUMERIC not null,
    [CONFIG_SNIM_PONUMBER] NUMERIC not null default 0,
    [CONFIG_SNIM_REZ2] NUMERIC not null default 0,
    [CONFIG_SNIMAT_POL1] NUMERIC not null default 0,
    [CONFIG_SNIMAT_POL2] NUMERIC not null default 0,
    [CONFIG_SNIMAT_POL3] NUMERIC not null default 0,
    [CONFIG_SNIMATZADAT_SN] NUMERIC not null default 0,
    [CONFIG_ZADAT_MN_POKAZDE] NUMERIC not null default 0,
    [ENABLE_LISTSNIM] NUMERIC not null default 0,
    [ENABLE_SNIMATZADAT_SN] NUMERIC not null default 0,
    [CONFIG_MNOZSTVI_PREDVYPLNIT] NUMERIC not null default 0,
    [CONFIG_MNOZSTVI_PREDVYPLNIT_JEDNA] NUMERIC not null default 0,
    [CONFIG_MNOZSTVI_PREDVYPLNIT_ZBYVAJICI] NUMERIC not null default 0,
    [CONFIG_MNOZSTVI_ZADAVAT] NUMERIC not null default 0,
    [CONFIG_MNOZSTVI_SCANNEREM] NUMERIC not null default 0,
    [CONFIG_LOKACE_POVOLIT] NUMERIC,
    [CONFIG_LOKACE_TIMEOUT] INTEGER,
    [CONFIG_LOKACE_PRIJMOVA] NUMERIC
);

