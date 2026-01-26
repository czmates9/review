create table [FASK_Events]
(
    [id] int identity not null,
    [loginid] nchar(20) not null,
    [machineid] nchar(20) not null,
    [dateeve] datetime not null,
    [qty] numeric(19,5) not null,
    [qtyReal] numeric(19,5) not null,
    [description] ntext,
    [barcodeReaded] nchar(50) not null,
    [barcodeSended] nchar(50) not null,
    [zakazka] nchar(20),
    [popis] nchar(10),
    [faskGUID] uniqueidentifier not null,
    [reportType] nchar(1) not null,
    [isProcessed] datetime,
    [IDO] nchar(10),
    [scan1] nvarchar(255),
    [scan2] nvarchar(255),
    [scan3] nvarchar(255),
    [sensor] nvarchar(50),
    [material] nvarchar(250),
    [VPH] nvarchar(30) null,     -- SOPNUMBE z tabulky CZPRO_VPH
    [VPPol] int null,            -- DEX_ROW_ID z tabulky CZPRO_VPP
    [EAN_IS] nvarchar(31) null,  -- BarcodeP z tabulky CZPRO_VPP
    [IS_ID] nvarchar(40) null,   -- ITEMNMBR z tabulky CZPRO_VPP
    [NMBRPAL] nvarchar(50) null,  -- SSCC èíslo palety
    [status] int null,            -- dle JaS status neèeho....
    [productionGuid] uniqueidentifier null,
    [QTYPACK] numeric(19, 5) NOT NULL DEFAULT (0),
	[PackType] nvarchar(50) NULL,
	[WEIGHT] numeric(19, 5) NULL,
    [BarcodeT] [tinyint] NOT NULL DEFAULT ((1)),
	[REZ_1] [nvarchar](100) NULL,
	[REZ_2] [nvarchar](100) NULL,
	[REZ_3] [nvarchar](100) NULL,
	[REZ_4] [nvarchar](100) NULL,
	[REZ_5] [nvarchar](100) NULL
);
alter table [FASK_Events] add primary key ([id]);
create index IDX_FASK_Events_faskGUID on [FASK_Events] ([faskGUID]);
create unique index UQ__FASK_Events__0000000000000047 on [FASK_Events] ([id]);

create table [FASK_EventsErr]
(
    [id] int not null,
    [loginid] nchar(20) not null,
    [machineid] nchar(20) not null,
    [dateeve] datetime not null,
    [qty] numeric(19,5) not null,
    [qtyReal] numeric(19,5) not null,
    [description] ntext,
    [barcodeReaded] nchar(50) not null,
    [barcodeSended] nchar(50) not null,
    [zakazka] nchar(20),
    [popis] nchar(10),
    [faskGUID] uniqueidentifier not null,
    [reportType] nchar(1) not null,
    [isProcessed] datetime,
    [IDO] nchar(10),
    [scan1] nvarchar(255),
    [scan2] nvarchar(255),
    [scan3] nvarchar(255),
    [sensor] nvarchar(50),
    [material] nvarchar(250)
);

create table [FASK_Logins]
(
    [USERID] nvarchar(20) not null,
    [firstname] nvarchar(20) not null,
    [surname] nvarchar(50) not null,
    [psswd] nvarchar(10) not null,
    [CREATED] datetime null,
    [VALIDFROM] datetime null,
    [VALIDTO] datetime null,
    [RFID] nvarchar(20) null
);
alter table [FASK_Logins] add primary key ([USERID]);
create unique index UQ__FASK_Logins__000000000000008D on [FASK_Logins] ([USERID]);

create table [FASK_Machines]
(
    [id] nchar(20) not null,
    [machinetype] nvarchar(10) not null,
    [name] nchar(10) not null,
    [description] nchar(50) not null,
    [koeficient] numeric(19,5) not null
);
alter table [FASK_Machines] add primary key ([id]);
create unique index UQ__FASK_Machines__00000000000000D0 on [FASK_Machines] ([id]);

create table [FASK_StatusType]
(
    [statusid] nchar(10) not null,
    [statusdesc] nvarchar(100) not null
);
alter table [FASK_StatusType] add primary key ([statusid]);

create table [FASK_UserEvents]
(
    [id] int identity not null,
    [loginid] nchar(20) not null,
    [machineid] nchar(20) not null,
    [dateeve] datetime not null,
    [statusid] nchar(10) not null,
    [faskGUID] uniqueidentifier not null,
    [rez_1] nvarchar(50),
    [rez_2] nvarchar(50)
);
alter table [FASK_UserEvents] add primary key ([id]);
create index IDX_FASK_UserEvents_faskGUID on [FASK_UserEvents] ([faskGUID]);
create unique index UQ__FASK_UserEvents__00000000000000A3 on [FASK_UserEvents] ([id]);

create table [FASK_UserEventsErr]
(
    [id] int not null,
    [loginid] nchar(20) not null,
    [machineid] nchar(20) not null,
    [dateeve] datetime not null,
    [statusid] nchar(10) not null,
    [faskGUID] uniqueidentifier not null,
    [rez_1] nvarchar(50),
    [rez_2] nvarchar(50)
);

CREATE TABLE [CZPRO_VPH](
	[CountEntries] int NOT NULL DEFAULT ('1'),
	[SOPNUMBE] nvarchar(30) NOT NULL,
	[SOPTYPE] nvarchar(11) NOT NULL DEFAULT (''),
	[SOPDESC] nvarchar(100) NULL,
	[VNDDOCNMH] nvarchar(21) NULL,
	[BarcodeH] nvarchar(31) NOT NULL,
	[LOCNCODE] nvarchar(11) NULL,
	[DateProd] smallint NOT NULL,
	[Rez1] nvarchar(50) NOT NULL,
	[Rez2] nvarchar(50) NOT NULL,
	[TermID] tinyint NOT NULL,
	[LSTMod] datetime NOT NULL,
	[DEX_ROW_ID] int NULL
);

CREATE TABLE [CZPRO_VPP](
	[CountEntries] int NOT NULL DEFAULT ('1'),
	[SOPNUMBE] nvarchar(30) NOT NULL,
	[ITEMNMBR] nvarchar(40) NOT NULL,
	[ITEMTYPE] nvarchar(11) NOT NULL DEFAULT (''),
	[ITEMDESC] nvarchar(100) NULL,
	[ITEMMJ] nvarchar(5) NULL,
	[VNDDOCNMP] nvarchar(21) NULL,
	[VNDITNUM] nvarchar(60) NULL,
	[ORD] int NOT NULL DEFAULT ((0)),
	[BarcodeP] nvarchar(31) NOT NULL,
	[LOCNCODE] nvarchar(11) NULL,
	[QTYSHPPD] numeric(19, 5) NOT NULL,
	[QTYPACK] numeric(19, 5) NOT NULL,
	[QTYPACKMJ] nvarchar(5) NULL,
    [TIMEPREP] real NOT NULL,
    [TIMEUNIT] real NOT NULL,
	[DtProdT] tinyint NOT NULL,
	[DtProdL] smallint NOT NULL,
	[SerNumT] tinyint NOT NULL,
	[SerNumL] smallint NOT NULL,
	[VerT] tinyint NOT NULL,
	[VerL] smallint NOT NULL,
	[TermID] tinyint NOT NULL,
	[LSTMod] datetime NOT NULL,
	[DEX_ROW_ID] int NULL,
	[QTYODVEDENO] numeric(19, 5) NOT NULL,
	[CNTODVEDENO] numeric(19, 5) NOT NULL,
    [TIMEMODE] int NOT NULL DEFAULT (0),
    [BarcodeT] tinyint NOT NULL DEFAULT (1)
);
