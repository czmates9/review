create table [Production]
(
    [CountEntries] INTEGER,
    [SOPNUMBE] TEXT,
    [ITEMNMBR] TEXT,
    [ITEMTYPE] TEXT default (''),
    [ITEMMJ] TEXT,
    [ORD] INTEGER,
    [TIMEPREP] REAL,
    [TIMEUNIT] REAL,
    [TIMESTART] TEXT,
    [TIMESTOP] TEXT,
    [TIMECOR] REAL,
    [TIMECRID] INTEGER,
    [id] INTEGER CONSTRAINT Production_ID_PK PRIMARY KEY,
    [loginid] TEXT not null,
    [machineid] TEXT,
    [dateeve] TEXT not null,
    [qty] NUMERIC not null,
    [qtyReal] NUMERIC not null,
    [QTYPACK] NUMERIC,
    [QTYPACKMJ] TEXT,
    [description] TEXT,
    [BarcodeP] TEXT,
    [UserID] TEXT not null,
    [TermID] INTEGER not null,
    [ISOK] TEXT,
    [GUID] uniqueidentifier not null,
    [TIMEMODE] INTEGER,
    [TIMEPREPSTART] TEXT,
    [TIMEPREPSTOP] TEXT,
    [TIMECORSTART] TEXT,
    [TIMECORSTOP] TEXT,
    [operationid] TEXT,
    [SOUBEHGUID] uniqueidentifier,
    [CORRGUID] uniqueidentifier,
    [TIMECRIDTYPE] INTEGER,
    [SKL_ID] TEXT,
    [LOCNCODE] TEXT,
    [idVS] TEXT,
    [qtyOld] NUMERIC,
    [ITEMDESC] TEXT,
    SERLTNUM TEXT,
    [NMBRPAL] TEXT null,
    [TYPEPAL] TEXT null,
    [PackType] TEXT null,
    [status] INTEGER null,
    [WEIGHT] NUMERIC null,
    [STORNOGUID] uniqueidentifier null,
    [REZ_1] TEXT null,
	[REZ_2] TEXT null,
	[REZ_3] TEXT null,
	[REZ_4] TEXT null,
	[REZ_5] TEXT null,
	[WEIGHT_OLD] NUMERIC null

);

create index IX_PRODUCTION on [Production] ([SOPNUMBE],[ITEMNMBR]);

create table [Production_Sources]
(
    [CountEntries] INTEGER,
    [SOPNUMBE] TEXT,
    [ITEMNAME] TEXT,
    [ITEMNMBR] TEXT,
    [ITEMTYPE] TEXT default (''),
    [ITEMCODE] TEXT,
    [LOCNCODE] TEXT,
    [MJ] TEXT not null,
    [QTYSHPPD] NUMERIC not null,
    [QTYSHPPDMJ] NUMERIC not null,
    [QTYPACK] NUMERIC,
    [SERLTNUM] TEXT not null,
    [GUID_Production] uniqueidentifier,
    [GUID] uniqueidentifier,
    [USER_ID] TEXT,
    [TERMINAL_ID] INTEGER not null,
    [DEX_ROW_ID] INTEGER null,
    [WEIGHT] NUMERIC,
    [NMBRPAL] TEXT,
    [TYPEPAL] TEXT,
    [PRINTED] INTEGER default (0),	-- priznak tisku (zatim se nevyuziva) NULL 
    [SKL_ID] TEXT
);

create unique index UQ__Production_Sources__0000000000000112 on [Production_Sources] ([GUID]);

create table [UserEvents]
(
    [id] INTEGER CONSTRAINT UserEvents_ID_PK PRIMARY KEY,
    [loginid] TEXT not null,
    [machineid] TEXT,
    [dateeve] TEXT not null,
    [statusid] TEXT not null,
    [UserID] TEXT not null,
    [TermID] INTEGER not null,
    [REZ1] TEXT,
    [GUID] uniqueidentifier not null
);

create table [Production_SN]
(
	[GUID_Production] uniqueidentifier not null,
	[SERLNMBR] TEXT not null,
	[ITEMNMBR] TEXT not null,
	[QTY] NUMERIC default (0),
	[Expirace] TEXT null ,
	[REZ_1] TEXT null ,
	[REZ_2] TEXT null ,
	[REZ_3] TEXT null ,
	[REZ_4] TEXT null ,
	[GUID] uniqueidentifier not null
);
