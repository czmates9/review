create table [Settings_Values]
(
    [KEY] TEXT not null PRIMARY KEY,
    [VALUE] TEXT not null
);

create index IDX_KEY on [Settings_Values] ([KEY]);