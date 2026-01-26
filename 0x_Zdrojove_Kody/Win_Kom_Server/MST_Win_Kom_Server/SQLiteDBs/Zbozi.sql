create table [CZMST095]
(
    [ITEMNMBR] TEXT not null,
    [ITEMDESC] TEXT,
    [VNDITNUM] TEXT,
    [CZ_CarKod] TEXT,
    [LOCNCODE] TEXT,
    [QTY] NUMERIC not null,
    [QTYPACK] NUMERIC,
    [TAXRATE] NUMERIC,
    [PRICE0] NUMERIC,
    [PRICE1] NUMERIC,
    [PRICE2] NUMERIC,
    [PRICE3] NUMERIC,
    [PRICE4] NUMERIC,
    [PRICE5] NUMERIC,
    [CZ_SerNum_Track] INTEGER not null,
    [CZ_SerNum_Delka] INTEGER not null,
    [CZ_Rez1_Track] INTEGER not null default 0,
    [CZ_Rez2_Track] INTEGER not null default 0,
    [CZ_Rez3_Track] INTEGER not null default 0,
    [CZ_Rez4_Track] INTEGER not null default 0,
    [DEX_ROW_ID] INTEGER not null,
    [SKL_ID] TEXT,
    [MJ] TEXT not null default '',
    [DMJ] TEXT not null default '',
    [REZ1] TEXT,
    [ITEMCODE] TEXT,
    [ODB_ID] TEXT,
    [REZ2] TEXT,
    [REZ3] TEXT,
    [REZ4] TEXT,
    [MENA_ID] TEXT,
    [SERLTNUM] TEXT,
    [WEIGHT] NUMERIC,
    [CZ_Expirace_Track] INTEGER not null,
    [EXPIRACE] TEXT null
);
create index IDX_CarkodHledani on [CZMST095] ([VNDITNUM],[CZ_CarKod]);
create index IDX_CarkodHledani2 on [CZMST095] ([CZ_CarKod],[VNDITNUM]);
create index IDX_CZCARKOD on [CZMST095] ([CZ_CarKod]);
create index IDX_ITEMCODE on [CZMST095] ([ITEMCODE]);
create index IDX_ITEMDESC on [CZMST095] ([ITEMDESC]);
create index IDX_ITEMNMBR on [CZMST095] ([ITEMNMBR]);
create index IDX_ODB_ID on [CZMST095] ([ODB_ID]);
create index IDX_SERLTNUM on [CZMST095] ([SERLTNUM]);
create index IDX_VNDITNUM on [CZMST095] ([VNDITNUM]);

create table [CZMST095M]
(
    [ITEMNMBR] TEXT not null,
    [mena_ID] TEXT not null,
    [PRICE] NUMERIC,
    [PRICEX] INTEGER,
    [DEX_ROW_ID] INTEGER not null
);
create index IDX_ITEM on [CZMST095M] ([ITEMNMBR]);
create index IDX_ItemMena on [CZMST095M] ([ITEMNMBR],[mena_ID]);
create index IDX_ItemMenaPricex on [CZMST095M] ([ITEMNMBR],[mena_ID],[PRICEX]);
create index IDX_Mena on [CZMST095M] ([mena_ID]);

