create table [fask_Vydej_Kontrola]
(
    [Faktura] nvarchar(50) not null,
    [BarcodeZboziFaktura] nvarchar(50) not null,
    [BarcodeZboziVydej] nvarchar(50) not null,
    [BarcodeZboziFakturaDatetime] datetime not null,
    [BarcodeZboziVydejDatetime] datetime not null,
    [UserID] nvarchar(50) not null,
    [TerminalID] nvarchar(50) not null,
    [GUID] uniqueidentifier not null PRIMARY KEY,
    [ServerDatetime] datetime not null
);
