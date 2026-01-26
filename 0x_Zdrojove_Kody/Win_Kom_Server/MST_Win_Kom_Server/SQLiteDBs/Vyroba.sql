create table [Corrects]
(
    [id] INTEGER CONSTRAINT Corrects_ID_PK PRIMARY KEY,
    [desc] TEXT not null,
    [TMFrom] REAL default (0),
    [TMTo] REAL,
    [Production] INTEGER not null default 1,
    [ProductionType] INTEGER
);

create table [CZMST093]
(
    [skl_id] TEXT not null,
    [skl_desc] TEXT,
    [skl_typ] TEXT,
    [skl_carcode] TEXT,
    [DEX_ROW_ID] INTEGER null
);
create index IDX_093_SKLAD on [CZMST093] ([skl_id]);
create index IDX_Barcode on [CZMST093] ([skl_carcode]);

create table [CZMST094]
(
    [SKL_ID] TEXT,
    [LOCNCODE] TEXT not null,
    [TYPE] TEXT,
    [Description] TEXT,
    [Barcode] TEXT,
    [DEX_ROW_ID] INTEGER null
);
create index IDX_094_SKLAD_Barcode on [CZMST094] ([SKL_ID],[Barcode]);
create index IDX_094_SKLAD_LOKACE on [CZMST094] ([SKL_ID],[LOCNCODE]);

create table [CZPRO_VPH]
(
    [CountEntries] INTEGER not null default ('1'),
    [SOPNUMBE] TEXT not null,
    [SOPTYPE] TEXT not null default (''),
    [SOPDESC] TEXT,
    [VNDDOCNMH] TEXT,
    [BarcodeH] TEXT not null,
    [LOCNCODE] TEXT,
    [DateProd] INTEGER not null,
    [Rez1] TEXT not null,
    [Rez2] TEXT not null,
    [TermID] INTEGER not null,
    [LSTMod] TEXT not null,
    [DEX_ROW_ID] INTEGER null,
    [USERID] INTEGER null
);
create index IX_CZPRO_VPH on [CZPRO_VPH] ([BarcodeH]);

create table [CZPRO_VPP]
(
    [CountEntries] INTEGER not null default ('1'),
    [SOPNUMBE] TEXT not null,
    [ITEMNMBR] TEXT not null,
    [ITEMTYPE] TEXT not null default (''),
    [ITEMDESC] TEXT,
    [ITEMMJ] TEXT,
    [VNDDOCNMP] TEXT,
    [VNDITNUM] TEXT,
    [ORD] INTEGER not null default (0),
    [BarcodeP] TEXT not null,
    [LOCNCODE] TEXT,
    [QTYSHPPD] NUMERIC not null,
    [QTYPACK] NUMERIC not null,
    [QTYPACKMJ] TEXT,
    [TIMEPREP] REAL not null,
    [TIMEUNIT] REAL not null,
    [DtProdT] INTEGER not null,
    [DtProdL] INTEGER not null,
    [SerNumT] INTEGER not null,
    [SerNumL] INTEGER not null,
    [VerT] INTEGER not null,
    [VerL] INTEGER not null,
    [TermID] INTEGER not null,
    [LSTMod] TEXT not null,
    [DEX_ROW_ID] INTEGER null,
    [QTYODVEDENO] NUMERIC not null default (0),
    [CNTODVEDENO] NUMERIC not null default (0),
    [TIMEMODE] INTEGER not null default (0),
    [BarcodeT] INTEGER not null default (1),
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
create index IX_CZPRO_VPP on [CZPRO_VPP] ([BarcodeP]);
create index IX_Update_Key on [CZPRO_VPP] ([SOPNUMBE],[ITEMNMBR]);

create table [FASK_CONS_095]
(
    [ITEMNMBR] TEXT not null,
    [ITEMDESC] TEXT,
    [VNDITNUM] TEXT,
    [CZ_CarKod] TEXT,
    [LOCNCODE] TEXT,
    [SKL_ID] TEXT,
    [QTY] NUMERIC not null,
    [QTYPACK] NUMERIC,
    [MJ] TEXT not null,
    [DMJ] TEXT,
    [TAXRATE] NUMERIC,
    [PRICE0] NUMERIC,
    [PRICE1] NUMERIC,
    [PRICE2] NUMERIC,
    [PRICE3] NUMERIC,
    [PRICE4] NUMERIC,
    [PRICE5] NUMERIC,
    [CZ_SerNum_Track] INTEGER not null,
    [CZ_SerNum_Delka] INTEGER not null,
    [CZ_Rez1_Track] INTEGER not null default 0,
    [CZ_Rez2_Track] INTEGER not null default 0,
    [CZ_Rez3_Track] INTEGER not null default 0,
    [CZ_Rez4_Track] INTEGER not null default 0,
    [REZ1] TEXT,
    [DEX_ROW_ID] INTEGER null,
    [ITEMCODE] TEXT,
    [ODB_ID] TEXT,
    [TIMEMODE] INTEGER not null default (0),
    [TIMEPREP] REAL not null,
    [TIMEUNIT] REAL not null,
    [TIMEFROM] TEXT,
    [TIMETO] TEXT,
    [LSTMod] TEXT not null,
    [loginid] TEXT not null
);
create index idx_czcarkod on [FASK_CONS_095] ([CZ_CarKod]);
create index idx_itemdesc on [FASK_CONS_095] ([ITEMDESC]);
create index idx_itemnmbr on [FASK_CONS_095] ([ITEMNMBR]);
create index idx_vnditnum on [FASK_CONS_095] ([VNDITNUM]);

create table [FASK_Vyroba_TP]
(
    [ID_H] TEXT,
    [ID_L] TEXT,
    [ITEMNMBR_Def] TEXT,
    [DESC_Def] TEXT,
    [MJ_Def] TEXT,
    [ITEMNMBR_fol] TEXT,
    [DESC_Fol] TEXT,
    [MJ_Fol] TEXT,
    [koef] TEXT,
    [ID_USER] TEXT,
    [dateedit] TEXT,
    [ID] INTEGER not null,
    [alter] TEXT,
    [PUO] TEXT
);
create index idx_idh on [FASK_Vyroba_TP] ([ID_H]);
create index idx_tp on [FASK_Vyroba_TP] ([ID_H],[ITEMNMBR_Def]);

create table [Logins]
(
    [id] TEXT not null CONSTRAINT Logins_ID_PK PRIMARY KEY,
    [firstname] TEXT not null,
    [surname] TEXT not null,
    [psswd] TEXT not null,
    [VS] INTEGER not null
);


create table [Machines]
(
    [id] TEXT not null CONSTRAINT Machines_ID_PK PRIMARY KEY,
    [name] TEXT not null,
    [description] TEXT not null
);


create table [Operations]
(
    [id] TEXT not null CONSTRAINT Operations_ID_PK PRIMARY KEY,
    [name] TEXT not null,
    [description] TEXT not null
);


create table [StatusTypes]
(
    [statusid] TEXT not null CONSTRAINT StatusTypes_ID_PK PRIMARY KEY,
    [statusdesc] TEXT
);


create table [VMachinesOperations]
(
    [machineid] TEXT not null,
    [operationid] TEXT not null
);

