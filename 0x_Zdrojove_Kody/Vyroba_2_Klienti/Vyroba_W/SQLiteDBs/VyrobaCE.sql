create table [Corrects]
(
    [id] int not null,
    [desc] nvarchar(10) not null,
    [TMFrom] real default (0),
    [TMTo] real,
    [Production] tinyint not null default 1,
    [ProductionType] tinyint,
	CONSTRAINT Corrects_PK primary key ([id])
);

create table [CZMST093]
(
    [skl_id] nvarchar(20) not null,
    [skl_desc] nvarchar(40),
    [skl_typ] nvarchar(3),
    [skl_carcode] nvarchar(21),
    [DEX_ROW_ID] int not null
);
create index IDX_093_SKLAD on [CZMST093] ([skl_id]);
create index IDX_Barcode on [CZMST093] ([skl_carcode]);

create table [CZMST094]
(
    [SKL_ID] nvarchar(20),
    [LOCNCODE] nvarchar(11) not null,
    [TYPE] nvarchar(2),
    [Description] nvarchar(40),
    [Barcode] nvarchar(21),
    [DEX_ROW_ID] int not null
);
create index IDX_094_SKLAD_Barcode on [CZMST094] ([SKL_ID],[Barcode]);
create index IDX_094_SKLAD_LOKACE on [CZMST094] ([SKL_ID],[LOCNCODE]);

create table [CZPRO_VPH]
(
    [CountEntries] int not null default ('1'),
    [SOPNUMBE] nvarchar(17) not null,
    [SOPTYPE] nvarchar(11) not null default (''),
    [SOPDESC] nvarchar(51),
    [VNDDOCNMH] nvarchar(21),
    [BarcodeH] nvarchar(31) not null,
    [LOCNCODE] nvarchar(11),
    [DateProd] smallint not null,
    [Rez1] nvarchar(11) not null,
    [Rez2] nvarchar(11) not null,
    [TermID] tinyint not null,
    [LSTMod] datetime not null,
    [DEX_ROW_ID] int identity not null
);
create index IX_CZPRO_VPH on [CZPRO_VPH] ([BarcodeH]);

create table [CZPRO_VPP]
(
    [CountEntries] int not null default ('1'),
    [SOPNUMBE] nvarchar(17) not null,
    [ITEMNMBR] nvarchar(31) not null,
    [ITEMTYPE] nvarchar(11) not null default (''),
    [ITEMDESC] nvarchar(51),
    [ITEMMJ] nvarchar(5),
    [VNDDOCNMP] nvarchar(21),
    [VNDITNUM] nvarchar(31),
    [ORD] int not null default (0),
    [BarcodeP] nvarchar(31) not null,
    [LOCNCODE] nvarchar(11),
    [QTYSHPPD] numeric not null,
    [QTYPACK] numeric not null,
    [QTYPACKMJ] nvarchar(5),
    [TIMEPREP] real not null,
    [TIMEUNIT] real not null,
    [DtProdT] tinyint not null,
    [DtProdL] smallint not null,
    [SerNumT] tinyint not null,
    [SerNumL] smallint not null,
    [VerT] tinyint not null,
    [VerL] smallint not null,
    [TermID] tinyint not null,
    [LSTMod] datetime not null,
    [DEX_ROW_ID] int identity not null,
    [QTYODVEDENO] numeric not null default (0),
    [CNTODVEDENO] numeric not null default (0),
    [TIMEMODE] int not null default 0
);
create index IX_CZPRO_VPP on [CZPRO_VPP] ([BarcodeP]);
create index IX_Update_Key on [CZPRO_VPP] ([SOPNUMBE],[ITEMNMBR]);

create table [FASK_CONS_095]
(
    [ITEMNMBR] nvarchar(31) not null,
    [ITEMDESC] nvarchar(51),
    [VNDITNUM] nvarchar(31),
    [CZ_CarKod] nvarchar(31),
    [LOCNCODE] nvarchar(11),
    [SKL_ID] nvarchar(20),
    [QTY] numeric not null,
    [QTYPACK] numeric,
    [MJ] nvarchar(5) not null,
    [DMJ] nvarchar(200),
    [TAXRATE] numeric,
    [PRICE0] numeric,
    [PRICE1] numeric,
    [PRICE2] numeric,
    [PRICE3] numeric,
    [PRICE4] numeric,
    [PRICE5] numeric,
    [CZ_SerNum_Track] tinyint not null,
    [CZ_SerNum_Delka] smallint not null,
    [CZ_Rez1_Track] tinyint not null default 0,
    [CZ_Rez2_Track] tinyint not null default 0,
    [CZ_Rez3_Track] tinyint not null default 0,
    [CZ_Rez4_Track] tinyint not null default 0,
    [REZ1] nvarchar(10),
    [DEX_ROW_ID] int identity not null,
    [ITEMCODE] nvarchar(50),
    [ODB_ID] nvarchar(12),
    [TIMEMODE] int not null default (0),
    [TIMEPREP] real not null,
    [TIMEUNIT] real not null,
    [TIMEFROM] datetime,
    [TIMETO] datetime,
    [LSTMod] datetime not null,
    [loginid] nvarchar(10) not null
);
create index idx_czcarkod on [FASK_CONS_095] ([CZ_CarKod]);
create index idx_itemdesc on [FASK_CONS_095] ([ITEMDESC]);
create index idx_itemnmbr on [FASK_CONS_095] ([ITEMNMBR]);
create index idx_vnditnum on [FASK_CONS_095] ([VNDITNUM]);

create table [FASK_Vyroba_TP]
(
    [ID_H] nvarchar(10),
    [ID_L] nvarchar(10),
    [ITEMNMBR_Def] nvarchar(31),
    [DESC_Def] nvarchar(51),
    [MJ_Def] nvarchar(51),
    [ITEMNMBR_fol] nvarchar(31),
    [DESC_Fol] nvarchar(51),
    [MJ_Fol] nvarchar(51),
    [koef] nvarchar(10),
    [ID_USER] nvarchar(10),
    [dateedit] datetime,
    [ID] int not null,
    [alter] nvarchar(1),
    [PUO] nvarchar(1)
);
create index idx_idh on [FASK_Vyroba_TP] ([ID_H]);
create index idx_tp on [FASK_Vyroba_TP] ([ID_H],[ITEMNMBR_Def]);

create table [Logins]
(
    [id] nvarchar(10) not null,
    [firstname] nvarchar(20) not null,
    [surname] nvarchar(50) not null,
    [psswd] nvarchar(10) not null,
    [VS] tinyint not null,
	CONSTRAINT Logins_PK primary key ([id])
);


create table [Machines]
(
    [id] nvarchar(16) not null,
    [name] nvarchar(20) not null,
    [description] nvarchar(50) not null,
	CONSTRAINT Machines_PK primary key ([id])
);


create table [Operations]
(
    [id] nvarchar(16) not null,
    [name] nvarchar(20) not null,
    [description] nvarchar(50) not null,
	CONSTRAINT Operations_PK primary key ([id])
);


create table [StatusTypes]
(
    [statusid] nvarchar(10) not null,
    [statusdesc] nvarchar(100),
	CONSTRAINT StatusTypes_PK primary key ([statusid])
);


create table [VMachinesOperations]
(
    [machineid] nvarchar(16) not null,
    [operationid] nvarchar(16) not null
);

