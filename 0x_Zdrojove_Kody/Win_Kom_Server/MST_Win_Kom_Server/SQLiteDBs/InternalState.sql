create table [LstOperationUser]
(
    [UserID] TEXT not null,
    [LastOper] TEXT not null
);
create unique index UQ__LstOperationUser__0000000000000006 on [LstOperationUser] ([UserID]);

create table [Production]
(
    [CountEntries] INTEGER,
    [SOPNUMBE] TEXT,
    [ITEMNMBR] TEXT,
    [ITEMTYPE] TEXT default (''),
    [ITEMMJ] TEXT,
    [ORD] INTEGER,
    [TIMEMODE] INTEGER default (0),
    [TIMEPREPSTART] TEXT,
    [TIMEPREPSTOP] TEXT,
    [TIMEPREP] REAL,
    [TIMEUNIT] REAL,
    [TIMESTART] TEXT,
    [TIMESTOP] TEXT,
    [TIMECORSTART] TEXT,
    [TIMECORSTOP] TEXT,
    [TIMECOR] REAL,
    [TIMECRID] INTEGER,
    [id] INTEGER CONSTRAINT Production_ID_PK PRIMARY KEY,
    [loginid] TEXT not null,
    [machineid] TEXT,
    [operationid] TEXT,
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
    [SOUBEHGUID] uniqueidentifier,
    [CORRGUID] uniqueidentifier,
    [qtyOld] NUMERIC,
    [idVS] TEXT,
    [dateedit] TEXT,
    [TIMECRIDTYPE] INTEGER,
    [SKL_ID] TEXT,
    [LOCNCODE] TEXT,
    [ITEMDESC] TEXT
);