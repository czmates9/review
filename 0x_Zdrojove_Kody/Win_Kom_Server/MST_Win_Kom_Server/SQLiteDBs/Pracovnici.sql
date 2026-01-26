create table [CZMST096]
(
    [prac_id] TEXT not null,
    [prac_desc] TEXT,
    [prac_typ] TEXT,
    [prac_carcode] TEXT,
    [DEX_ROW_ID] INTEGER not null
);
create index IDX_CARCODE on [CZMST096] ([prac_carcode]);
create index IDX_DESC on [CZMST096] ([prac_desc]);
create index IDX_ID on [CZMST096] ([prac_id]);

