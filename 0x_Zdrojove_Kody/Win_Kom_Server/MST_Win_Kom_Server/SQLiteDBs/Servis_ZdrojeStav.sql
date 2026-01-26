create table [CZMST_Servis_Predloha]
(
    [CountEntries] INTEGER not null,
    [DOCUMENT_NUMBER] TEXT,
    [Rozpracovano] INTEGER not null,
    [OkruhID] TEXT not null,
    [UserID] INTEGER,
    [Barcode] TEXT,
    [ODB_ID] TEXT
);

create table [CZMST_Servis_ZdrojStav]
(
    [IDZdroj] TEXT not null,
    [IDStav] TEXT not null,
    [IDCinnost] TEXT,
    [Modified] TEXT not null,
    [IDTerminal] INTEGER,
    [IDUser] INTEGER,
    [GUID] uniqueidentifier,
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

create table [CZMST_Servis_ZdrojStavTmp]
(
    [ZdrojID] TEXT CONSTRAINT CZMST_Servis_ZdrojStavTmp_ZdrojID_PK PRIMARY KEY,
    [Rozpracovano] INTEGER not null default 0,
    [Dokonceno] TEXT
);

create table [Parametry]
(
    [CONFIG_KONT_DOKONCENOSTI] NUMERIC not null
);



