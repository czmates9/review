create table [FASK_Events]
(
    [id] INTEGER CONSTRAINT FASK_Events_ID_PK PRIMARY KEY,
    [loginid] TEXT not null,
    [machineid] TEXT not null,
    [dateeve] TEXT not null,
    [qty] NUMERIC not null,
    [qtyReal] NUMERIC not null,
    [description] TEXT,
    [barcodeReaded] TEXT not null,
    [barcodeSended] TEXT not null,
    [zakazka] TEXT,
    [popis] TEXT,
    [faskGUID] uniqueidentifier not null,
    [reportType] TEXT not null,
    [isProcessed] TEXT,
    [IDO] TEXT,
    [scan1] TEXT,
    [scan2] TEXT,
    [scan3] TEXT,
    [sensor] TEXT,
    [material] TEXT,
    [VPH] TEXT null,            
    [VPPol] INTEGER null,      
    [EAN_IS] TEXT null,        
    [IS_ID] TEXT null,   
    [NMBRPAL] TEXT null, 
    [status] INTEGER null,
    [productionGuid] uniqueidentifier null,
    [QTYPACK] NUMERIC NOT NULL DEFAULT (0),
	[PackType] TEXT NULL,
	[WEIGHT] NUMERIC NULL,
    [BarcodeT] INTEGER NOT NULL DEFAULT (1),
	[REZ_1] TEXT NULL,
	[REZ_2] TEXT NULL,
	[REZ_3] TEXT NULL,
	[REZ_4] TEXT NULL,
	[REZ_5] TEXT NULL
);

create index IDX_FASK_Events_faskGUID on [FASK_Events] ([faskGUID]);
create unique index UQ__FASK_Events__0000000000000047 on [FASK_Events] ([id]);

create table [FASK_EventsErr]
(
    [id] INTEGER CONSTRAINT FASK_EventsErr_ID_PK PRIMARY KEY,
    [loginid] TEXT not null,
    [machineid] TEXT not null,
    [dateeve] TEXT not null,
    [qty] NUMERIC not null,
    [qtyReal] NUMERIC not null,
    [description] TEXT,
    [barcodeReaded] TEXT not null,
    [barcodeSended] TEXT not null,
    [zakazka] TEXT,
    [popis] TEXT,
    [faskGUID] uniqueidentifier not null,
    [reportType] TEXT not null,
    [isProcessed] TEXT,
    [IDO] TEXT,
    [scan1] TEXT,
    [scan2] TEXT,
    [scan3] TEXT,
    [sensor] TEXT,
    [material] TEXT
);

create table [FASK_Logins]
(
    [USERID] TEXT CONSTRAINT FASK_Logins_USERID_PK PRIMARY KEY,
    [firstname] TEXT not null,
    [surname] TEXT not null,
    [psswd] TEXT not null,
    [CREATED] TEXT null,
    [VALIDFROM] TEXT null,
    [VALIDTO] TEXT null,
    [RFID] TEXT null
);

create unique index UQ__FASK_Logins__000000000000008D on [FASK_Logins] ([USERID]);

create table [FASK_Machines]
(
    [id] TEXT CONSTRAINT FASK_Machines_ID_PK PRIMARY KEY,
    [machinetype] TEXT not null,
    [name] TEXT not null,
    [description] TEXT not null,
    [koeficient] NUMERIC not null
);

create unique index UQ__FASK_Machines__00000000000000D0 on [FASK_Machines] ([id]);

create table [FASK_StatusType]
(
    [statusid] TEXT CONSTRAINT FASK_StatusType_STATUSID_PK PRIMARY KEY,
    [statusdesc] TEXT not null
);
 
create table [FASK_UserEvents]
(
    [id] INTEGER CONSTRAINT FASK_UserEvents_ID_PK PRIMARY KEY,
    [loginid] TEXT not null,
    [machineid] TEXT not null,
    [dateeve] TEXT not null,
    [statusid] TEXT not null,
    [faskGUID] uniqueidentifier not null,
    [rez_1] TEXT,
    [rez_2] TEXT
);

create index IDX_FASK_UserEvents_faskGUID on [FASK_UserEvents] ([faskGUID]);
create unique index UQ__FASK_UserEvents__00000000000000A3 on [FASK_UserEvents] ([id]);

create table [FASK_UserEventsErr]
(
    [id] INTEGER CONSTRAINT FASK_UserEventsErr_ID_PK PRIMARY KEY,
    [loginid] TEXT not null,
    [machineid] TEXT not null,
    [dateeve] TEXT not null,
    [statusid] TEXT not null,
    [faskGUID] uniqueidentifier not null,
    [rez_1] TEXT,
    [rez_2] TEXT
);

CREATE TABLE [CZPRO_VPH](
	[CountEntries] INTEGER NOT NULL DEFAULT ('1'),
	[SOPNUMBE] TEXT NOT NULL,
	[SOPTYPE] TEXT NOT NULL DEFAULT (''),
	[SOPDESC] TEXT NULL,
	[VNDDOCNMH] TEXT NULL,
	[BarcodeH] TEXT NOT NULL,
	[LOCNCODE] TEXT NULL,
	[DateProd] INTEGER NOT NULL,
	[Rez1] TEXT NOT NULL,
	[Rez2] TEXT NOT NULL,
	[TermID] INTEGER NOT NULL,
	[LSTMod] TEXT NOT NULL,
	[DEX_ROW_ID] INTEGER NULL,
	[USERID] INTEGER null
);

CREATE TABLE [CZPRO_VPP](
	[CountEntries] INTEGER NOT NULL DEFAULT ('1'),
	[SOPNUMBE] TEXT NOT NULL,
	[ITEMNMBR] TEXT NOT NULL,
	[ITEMTYPE] TEXT NOT NULL DEFAULT (''),
	[ITEMDESC] TEXT NULL,
	[ITEMMJ] TEXT NULL,
	[VNDDOCNMP] TEXT NULL,
	[VNDITNUM] TEXT NULL,
	[ORD] INTEGER NOT NULL DEFAULT (0),
	[BarcodeP] TEXT NOT NULL,
	[LOCNCODE] TEXT NULL,
	[QTYSHPPD] NUMERIC NOT NULL,
	[QTYPACK] NUMERIC NOT NULL,
	[QTYPACKMJ] TEXT NULL,
	[TIMEPREP] REAL NOT NULL,
	[TIMEUNIT] REAL NOT NULL,
	[DtProdT] INTEGER NOT NULL,
	[DtProdL] INTEGER NOT NULL,
	[SerNumT] INTEGER NOT NULL,
	[SerNumL] INTEGER NOT NULL,
	[VerT] INTEGER NOT NULL,
	[VerL] INTEGER NOT NULL,
	[TermID] INTEGER NOT NULL,
	[LSTMod] TEXT NOT NULL,
	[DEX_ROW_ID] INTEGER NULL,
	[QTYODVEDENO] NUMERIC NOT NULL,
	[CNTODVEDENO] NUMERIC NOT NULL,
	[TIMEMODE] INTEGER NOT NULL DEFAULT (0),
	[BarcodeT] INTEGER NOT NULL DEFAULT (1),
	[CZ_REZ1_Track] INTEGER not null default (0),
	[CZ_REZ2_Track] INTEGER not null default (0),
	[CZ_REZ3_Track] INTEGER not null default (0),
	[CZ_REZ4_Track] INTEGER not null default (0),
	[CZ_REZ5_Track] INTEGER not null default (0),
	[WEIGHT_TARA] NUMERIC null,
	[WEIGHT_NETTO] NUMERIC null,
	[WEIGHT_TOL_PLUS] NUMERIC null,
	[WEIGHT_TOL_MINUS] NUMERIC null
);
