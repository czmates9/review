create table [CZMST090]
(
    [odb_id] TEXT not null,
    [odb_desc] TEXT,
    [odb_typ] TEXT,
    [odb_carcode] TEXT,
    [DEX_ROW_ID] INTEGER not null,
    [odb_ico] TEXT,
    [mena_ID] TEXT,
    [odb_misto] TEXT default '',
    [odb_ulice] TEXT default '',
    [odb_cisloOr] TEXT default '',
    [odb_psc] TEXT default '',
    [odb_dic] TEXT default '',
    [odb_Odberatel] NUMERIC default 0,
    [odb_Dodavatel] NUMERIC default 0
);
create index IDX_CARCODE on [CZMST090] ([odb_carcode]);
create index IDX_DESC on [CZMST090] ([odb_desc]);
create index IDX_ID on [CZMST090] ([odb_id]);

