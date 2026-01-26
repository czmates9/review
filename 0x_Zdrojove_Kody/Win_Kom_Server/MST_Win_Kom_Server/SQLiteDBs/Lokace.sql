create table [CZMST_SkladLokace_LokaceTypy]
(
    [TYPE] TEXT,
    [Description] TEXT,
    [IS_RECEIVE] INTEGER not null,
    [IS_DEFAULT] INTEGER not null,
    [IS_NORMAL] INTEGER not null
);

create table [CZMST094]
(
    [SKL_ID] TEXT,
    [LOCNCODE] TEXT not null,
    [TYPE] TEXT,
    [DEX_ROW_ID] INTEGER,
    [Description] TEXT,
    [Barcode] TEXT
);
create index IX_skl_id on [CZMST094] ([SKL_ID]);

