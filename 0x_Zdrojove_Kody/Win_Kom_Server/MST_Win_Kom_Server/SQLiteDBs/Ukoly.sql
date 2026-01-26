create table [CZ_UKOL]
(
    [ID] INTEGER CONSTRAINT CZ_UKOL_ID_PK PRIMARY KEY,
    [Name] TEXT not null,
    [Description] TEXT not null,
    [Code] TEXT,
    [CreatorID] INTEGER not null,
    [DateCreated] TEXT not null,
    [DateFrom] TEXT,
    [DateTo] TEXT,
    [State] TEXT not null,
    [Kind] TEXT,
    [Type] TEXT,
    [Priority] INTEGER not null,
    [PartnerID] TEXT
);

create table [CZ_UKOL_STATE]
(
    [State] TEXT CONSTRAINT CZ_UKOL_STATE_State_PK PRIMARY KEY,
    [Description] TEXT not null,
    [IsStart] NUMERIC not null,
    [IsEnd] NUMERIC not null
);

create table [CZ_UKOL_UZIV]
(
    [ID] INTEGER CONSTRAINT CZ_UKOL_UZIV_ID_PK PRIMARY KEY,
    [UkolID] INTEGER not null,
    [UserID] INTEGER not null,
    [State] TEXT not null,
    [DateChanged] TEXT,
    [UserIDChanged] INTEGER,
    [Note] TEXT,
    [DateNotify] TEXT,
    [DateFinished] TEXT
);

