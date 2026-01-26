create table [CZMST091]
(
    [str_id] TEXT not null,
    [str_desc] TEXT,
    [str_typ] TEXT,
    [str_carcode] TEXT,
    [DEX_ROW_ID] INTEGER not null
);
create index IDX_CARCODE on [CZMST091] ([str_carcode]);
create index IDX_DESC on [CZMST091] ([str_desc]);
create index IDX_ID on [CZMST091] ([str_id]);

