create table [CZMST097]
(
    [mena_ID] TEXT CONSTRAINT meny_id_PK PRIMARY KEY,
    [mena_text] TEXT not null,
    [mena_hlavni] NUMERIC not null default 0,
    [mena_kurz] NUMERIC,
    [mena_kurzDatum] TEXT
);

