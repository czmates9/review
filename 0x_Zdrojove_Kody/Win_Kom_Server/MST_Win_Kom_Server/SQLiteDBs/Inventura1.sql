create table [CZMST_I1]
(
    [CountEntries] INTEGER not null,
    [ITEMNMBR] TEXT not null,
    [CZ_CarKod] TEXT not null,
    [ITEMDESC] TEXT not null,
    [LOCNCODE] TEXT not null,
    [QUANTITY] NUMERIC not null,
    [DATEDONE] TEXT not null,
    [IntegerValue] INTEGER not null,
    [TIMESPRT] INTEGER not null,
    [CZ_SerNum_Track] INTEGER not null,
    [CZ_SerNum_Find] INTEGER not null,
    [DEX_ROW_ID] INTEGER not null,
    [TerminalID] INTEGER not null,
    [O_TID] INTEGER,
    [skl_id] TEXT,
    [DMJ] TEXT default '',
    [REZ_1] TEXT,
    [REZ_2] TEXT,
    [ITEMCODE] TEXT,
    [CZ_REZ1_Track] INTEGER,
    [CZ_REZ2_Track] INTEGER,
	[CZ_Expirace_Track] INTEGER
);
create index IDX1_CZCarKod on [CZMST_I1] ([CZ_CarKod]);
create index IDX1_ITEMNMBR on [CZMST_I1] ([ITEMNMBR]);

create table [CZMST_I1H]
(
    [CountEntries] INTEGER CONSTRAINT CZMST_I1H_CountEntries_PK PRIMARY KEY,
    [Description] TEXT,
    [Status] INTEGER not null default 0
);


create table [CZMST_I2]
(
    [CountEntries] INTEGER not null,
    [ITEMNMBR] TEXT not null,
    [SERLNMBR] TEXT not null,
    [DEX_ROW_ID] INTEGER not null,
    [QTY] NUMERIC,
    [Expirace] TEXT
);
create index IDX2_ITEMNMBR on [CZMST_I2] ([ITEMNMBR]);
create index IDX2_SERLNMBR on [CZMST_I2] ([SERLNMBR]);

create table [CZMST_I3]
(
    [CountEntries] INTEGER,
    [ITEMNMBR] TEXT,
    [CZ_CarKod] TEXT,
    [QTYPACK] NUMERIC,
    [VENDORID] TEXT not null,
    [VNDITNUM] TEXT,
    [VENDNAME] TEXT not null,
    [DEX_ROW_ID] INTEGER not null,
    [MJ] TEXT not null default '',
    [WEIGHT] NUMERIC
);
create index IDX3_1 on [CZMST_I3] ([CZ_CarKod],[VNDITNUM]);
create index IDX3_2 on [CZMST_I3] ([VNDITNUM],[CZ_CarKod]);
create index IDX3_CZCarKod on [CZMST_I3] ([CZ_CarKod]);
create index IDX3_ITEMNMBR on [CZMST_I3] ([ITEMNMBR]);
create index IDX3_VNDITNUM on [CZMST_I3] ([VNDITNUM]);

create table [CZMST_I4]
(
    [CountEntries] INTEGER not null,
    [ITEMNMBR] TEXT not null,
    [CZ_CarKod] TEXT not null,
    [LOCNCODE] TEXT not null,
    [VNDITNUM] TEXT not null,
    [QUANTITY] NUMERIC not null,
    [QTYPACK] NUMERIC,
    [SERLNMBR] TEXT not null,
    [DATEDONE] TEXT,
    [TIMEDONE] TEXT,
    [USERID] INTEGER,
    [DEX_ROW_ID] INTEGER not null,
    [GUID] uniqueidentifier,
    [O_Checked] NUMERIC not null default 0,
    [skl_id] TEXT,
    [MJ] TEXT not null default '',
    [QUANTITYMJ] NUMERIC not null,
    [INPUT_MODE] INTEGER not null,
    [ID_TERMINAL] INTEGER not null,
    [ITEMCODE] TEXT,
    [REZ_1] TEXT,
    [REZ_2] TEXT,
    [WEIGHT] NUMERIC,
    [Expirace] TEXT
);
create index IDX4_DATUMCAS on [CZMST_I4] ([DATEDONE],[TIMEDONE]);
create index IDX4_ITEMNMBR on [CZMST_I4] ([ITEMNMBR]);
create index IDX4_SERLNMBR on [CZMST_I4] ([SERLNMBR]);

create table [CZMST_IH]
(
    [CountEntries] INTEGER not null,
    [GUID] uniqueidentifier not null
);

create table [Parametry]
(
    [CFG_UpozornitNaPrebytek] NUMERIC not null,
    [CFG_DalsiPolozkuBezDotazu] NUMERIC not null,
    [CFG_KontrolaUplnostiPolozky] NUMERIC not null,
    [CFG_PoZadaniSNZpetNaMN] NUMERIC not null,
    [CFG_PredvyplnitMnozstvi] NUMERIC not null,
    [CFG_PredvyplnitMnozstviZbyvajici] NUMERIC not null,
    [CFG_PredvyplnitMnozstviOJedna] NUMERIC not null,
    [CFG_PovolitDuplicituSN] NUMERIC not null,
    [CFG_MnozstviScannerem] NUMERIC not null,
    [CFG_PosunNaDalsiPolozku] NUMERIC not null,
    [CFG_PovolitZaporneMnozstvi] NUMERIC not null,
    [CFG_KontrolaUplnosti] NUMERIC not null,
    [CFG_PovolitZmenuLokace] NUMERIC not null default 0,
    [CFG_PovolitZobrazeniMnozstviNaSklade] NUMERIC,
    [CONFIG_LOKACE_POVOLIT] NUMERIC,
    [CONFIG_LOKACE_TIMEOUT] INTEGER
);

