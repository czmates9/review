create table [FASK_Events]
(
    [id] int identity not null,
    [loginid] nchar(20) not null,
    [dateeve] datetime not null,
    [qty] numeric(19,5) not null,
    [qtyReal] numeric(19,5) not null,
    [description] ntext(536870911),
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
    [machineid] nchar(20) not null
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
    [description] ntext(536870911),
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
    [machineid] nchar(20) not null
);
alter table [FASK_EventsErr] add primary key ([id]);

create table [FASK_Logins]
(
    [id] nchar(20) not null,
    [firstname] nvarchar(20) not null,
    [surname] nchar(50) not null,
    [psswd] nchar(10) not null,
    [rfid] nchar(20),
    [barcode] nchar(20)
);
alter table [FASK_Logins] add primary key ([id]);
create unique index UQ__FASK_Logins__000000000000008D on [FASK_Logins] ([id]);

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
    [rez_1] nchar(25),
    [rez_2] nchar(25)
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
    [rez_1] nchar(25),
    [rez_2] nchar(25)
);
