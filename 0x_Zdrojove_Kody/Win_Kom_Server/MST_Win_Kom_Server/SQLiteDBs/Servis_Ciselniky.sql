create table [CZMST_Servis_Cinnost]
(
    [ID] TEXT not null,
    [Oznaceni] TEXT not null,
    [Barcode] TEXT,
    [TYPE] TEXT not null,
    [TYPEVALUE] TEXT,
    [Mandatory] INTEGER not null,
    [RequiredLength] INTEGER
);

create table [CZMST_Servis_CinnostNext]
(
    [ID] TEXT not null,
    [IDNext] TEXT,
    [IDValue] TEXT
);

create table [CZMST_Servis_Dynamic_Table]
(
    [ID] TEXT CONSTRAINT CZMST_Servis_Dynamic_Table_ID_PK PRIMARY KEY,
    [Oznaceni] TEXT not null,
    [Barcode] TEXT
);

create table [CZMST_Servis_Dynamic_Table_Definition]
(
    [FullName] TEXT,
    [TypeName] TEXT,
	CONSTRAINT CZMST_Servis_Dynamic_Table_Definition_PK PRIMARY KEY ([FullName],[TypeName])
);

create table [CZMST_Servis_Okruh]
(
    [ID] TEXT CONSTRAINT CZMST_Servis_Okruh_ID_PK PRIMARY KEY,
    [Oznaceni] TEXT not null,
    [ODB_ID] TEXT,
    [Barcode] TEXT,
    [ZdrojSeznamID] TEXT not null
);

create table [CZMST_Servis_Stav]
(
    [ID] TEXT not null,
    [Oznaceni] TEXT not null,
    [IDCinnost] TEXT,
    [Barcode] TEXT
);

create table [CZMST_Servis_StavNext]
(
    [ID] TEXT not null,
    [IDNext] TEXT not null
);

create table [CZMST_Servis_Zdroj]
(
    [ID] TEXT not null,
    [Oznaceni] TEXT not null,
    [Barcode] TEXT,
    [Type] TEXT,
    [Misto] TEXT
);

create table [CZMST_Servis_ZdrojSeznam]
(
    [ID] TEXT,
    [ZdrojID] TEXT,
    [Poradi] INTEGER,
    [IDStav] TEXT,
    [IDCinnost] TEXT,
	CONSTRAINT CZMST_Servis_ZdrojSeznam_PK PRIMARY KEY ([ID], [ZdrojID])
);