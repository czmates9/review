create table [CZMST_DEH]
(
    [CountEntries] INTEGER CONSTRAINT CZMST_DEH_CountEntries_PK PRIMARY KEY,
    [GUID] uniqueidentifier
);

create table [CZMST_DI]
(
    [CountEntries] INTEGER not null,
    [ODB_ID] TEXT,
    [STR_ID] TEXT,
    [DOC_ID] TEXT,
    [ITEMNMBR] TEXT not null,
    [LOCNCODE] TEXT,
    [QTYSHPPD] NUMERIC not null,
    [QTYPACK] NUMERIC,
    [SERLTNUM] TEXT not null,
    [TAXAMPIE] NUMERIC,
    [AMOUNPIE] NUMERIC,
    [WITHTAX] INTEGER,
    [PRICEX] INTEGER,
    [REZ_1] TEXT,
    [REZ_2] TEXT,
    [REZ_3] TEXT,
    [REZ_4] TEXT,
    [USER_ID] INTEGER,
    [DATEDONE] TEXT,
    [TIMEDONE] TEXT,
    [DEX_ROW_ID] INTEGER,
    [guid] uniqueidentifier not null,
    [VNDITNUM] TEXT,
    [CZ_CarKod] TEXT,
    [SKL_ID] TEXT,
    [MJ] TEXT not null default '',
    [QTYSHPPDMJ] NUMERIC not null,
    [PRAC_ID] TEXT,
    [INPUT_MODE] INTEGER not null,
    [ID_TERMINAL] INTEGER not null,
    [TYPEPAL] TEXT,
    [NMBRPAL] TEXT,
    [ITEMCODE] TEXT,
    [DOC_ID2] TEXT default '',
    [mena_ID] TEXT,
    [TAXAMPIEM] NUMERIC,
    [AMOUNPIEM] NUMERIC,
    [mena_IDM] TEXT,
    [ITEMDESC] TEXT,
    [LOCNCODEDEST] TEXT,
    [SKL_ID_DEST] TEXT,
    [WEIGHT] NUMERIC,
    [PRINTED] NUMERIC not null,
    [EXPIRACE] TEXT null,
    [AttributeToSN] TEXT NULL
);
create index IDX_ITEMNMBR on [CZMST_DI] ([ITEMNMBR]);

create table [CZMST_DI_RFID]
(
    [ID] INTEGER identity not null,
    [ITEMNMBR] TEXT not null,
    [SEQUENCENMBR] INTEGER not null,
    [SKL_ID] TEXT,
    [CountEntries] INTEGER,
    [DOCUMENTNMBR] TEXT,
    [ORD] INTEGER,
    [SERLNMBR] TEXT,
    [guid] uniqueidentifier not null,
    [M_ID] TEXT,
    [M_TID] TEXT,
    [M_EPC] TEXT,
    [M_USER] TEXT,
    [M_RESERVED] TEXT,
    [O_M_ID] TEXT,
    [O_M_TID] TEXT,
    [O_M_EPC] TEXT,
    [O_M_USER] TEXT,
    [O_M_RESERVED] TEXT,
    [TerminalID] INTEGER not null,
    [UserID] INTEGER not null,
    [Created_T] TEXT not null
);
create unique index UQ__CZMST_DI_RFID__00000000000001B6 on [CZMST_DI_RFID] ([guid]);

create table [CZMST_DIH]
(
    [Zakazka_ID] TEXT,
    [Paleta_ID] TEXT,
    [mena_ID] TEXT,
    [SKL_ID] TEXT,
    [CountEntries] INTEGER not null
);

