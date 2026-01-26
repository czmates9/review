create table [CZMST_Servis_ZdrojPohyb]
(
    [IDZdroj] TEXT not null,
    [IDStav] TEXT not null,
    [IDCinnost] TEXT,
    [Modified] TEXT not null,
    [IDTerminal] INTEGER not null,
    [IDUser] INTEGER not null,
    [GUID] uniqueidentifier not null,
    [CinnostValue] TEXT,
    [CinnostType] TEXT,
    [CountEntries] INTEGER,
    [ODB_ID] TEXT,
    [OkruhID] TEXT,
    [CinnostOznaceni] TEXT,
    [GPS_X] REAL,
    [GPS_Y] REAL,
    [GPS_Z] INTEGER
);

