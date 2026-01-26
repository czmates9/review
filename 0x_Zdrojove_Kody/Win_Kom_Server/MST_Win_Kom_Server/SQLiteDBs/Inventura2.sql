create table [HLAVICKY]
(
    [ID] INTEGER not null,
    [GUID] uniqueidentifier not null
);

create table [INVENTUR]
(
    [ID] INTEGER CONSTRAINT INVENTUR_ID_PK PRIMARY KEY,
    [KATEGORIE] TEXT,
    [I_CISLO] TEXT,
    [NAZEV] TEXT,
    [STRED] TEXT,
    [OSOBA] INTEGER,
    [LOKACE1] TEXT,
    [LOKACE2] TEXT,
    [KANCELAR] TEXT,
    [EAN] TEXT,
    [KUSU] NUMERIC,
    [KLIC_LOK] INTEGER,
    [ID_INV] NUMERIC,
    [OS_ZPR] TEXT,
    [ID_TERM] NUMERIC,
    [CAS_ZPR] TEXT,
    [ID_MAJETEK] INTEGER not null
);
create index IDX_INVENTUR_EAN on [INVENTUR] ([EAN]);
create index IDX_INVENTUR_CASZPR on [INVENTUR] ([CAS_ZPR]);
create index IDX_INVENTUR_ICISLO on [INVENTUR] ([I_CISLO]);
create index IDX_INVENTUR_IDMAJETEK on [INVENTUR] ([ID_MAJETEK]);
create index IDX_INVENTUR_KANCELAR on [INVENTUR] ([KANCELAR]);
create index IDX_INVENTUR_KATEGORIE on [INVENTUR] ([KATEGORIE]);
create index IDX_INVENTUR_LOKACE on [INVENTUR] ([KLIC_LOK]);
create index IDX_INVENTUR_NAZEV on [INVENTUR] ([NAZEV]);
create index IDX_INVENTUR_OSOBA on [INVENTUR] ([OSOBA]);
create index IDX_INVENTUR_STRED on [INVENTUR] ([STRED]);
create index IDX_INVENTUR_UZIVATEL on [INVENTUR] ([OS_ZPR]);
create index IDX_INVENTUR_ZAZNAM on [INVENTUR] ([KATEGORIE],[I_CISLO]);

create table [KANCL]
(
    [KANCL] TEXT CONSTRAINT KANCL_KANCL_PK PRIMARY KEY,
    [STRE] TEXT,
    [TEXT] TEXT,
    [NAZEV] TEXT,
    [EAN] TEXT
);
create index IDX_KANCL_TEXT on [KANCL] ([TEXT]);

create table [LOKACE]
(
    [LOKACE1] TEXT,
    [LOKACE2] TEXT,
    [EANL] TEXT,
    [NAZEV] TEXT not null,
    [KLIC_LOK] INTEGER CONSTRAINT LOKACE_KLIC_LOK_PK PRIMARY KEY,
    [KLIC_KAT] INTEGER,
    [KLIC_K_OLD] INTEGER
);
create index IDX_LOKACE_EANL on [LOKACE] ([EANL]);
create index IDX_LOKACE_NAZEV on [LOKACE] ([NAZEV]);

create table [MAJETEK]
(
    [ID] INTEGER CONSTRAINT MAJETEK_ID_PK PRIMARY KEY,
    [KATEGORIE] TEXT,
    [I_CISLO] TEXT,
    [NAZEV] TEXT,
    [STRED] TEXT,
    [OSOBA] INTEGER,
    [LOKACE1] TEXT,
    [LOKACE2] TEXT,
    [KANCELAR] TEXT,
    [EAN] TEXT,
    [KUSU] NUMERIC,
    [KLIC_LOK] INTEGER,
    [ID_INV] TEXT,
    [ID_TERM] NUMERIC,
    [NACTENO] NUMERIC default 0
);
create index IDX_MAJETEK_EAN on [MAJETEK] ([EAN]);
create index IDX_MAJETEK_KANCELAR on [MAJETEK] ([KANCELAR]);
create index IDX_MAJETEK_KLICLOK on [MAJETEK] ([KLIC_LOK]);
create index IDX_MAJETEK_NAZEV on [MAJETEK] ([NAZEV]);
create index IDX_MAJETEK_OSOBA on [MAJETEK] ([OSOBA]);
create index IDX_MAJETEK_STRED on [MAJETEK] ([STRED]);
create index IDX_MAJETEK_ZAZNAM on [MAJETEK] ([KATEGORIE],[I_CISLO]);

create table [OSOBY]
(
    [OSOBA_ZODP] INTEGER CONSTRAINT OSOBY_OSOBA_ZODP_PK PRIMARY KEY,
    [TITUL] TEXT,
    [PRIJMENI] TEXT,
    [JMENO] TEXT
);

create index IDX_OSOBY_JMENO on [OSOBY] ([JMENO]);
create index IDX_OSOBY_PRIJMENI on [OSOBY] ([PRIJMENI]);
create index IDX_OSOBY_TITUL on [OSOBY] ([TITUL]);

create table [Parametry]
(
    [CFG_UpozornitNaPrebytek] NUMERIC not null,
    [CFG_DalsiPolozkuBezDotazu] NUMERIC not null,
    [CFG_KontrolaUplnostiPolozky] NUMERIC not null,
    [CFG_PoZadaniSNZpetNaMN] NUMERIC not null,
    [CFG_PredvyplnitMnozstvi] NUMERIC not null,
    [CFG_PredvyplnitMnozstviZbyvajici] NUMERIC not null,
    [CFG_PredvyplnitMnozstviOJedna] NUMERIC not null,
    [CFG_PovolitDuplicituSN] NUMERIC not null,
    [CFG_MnozstviScannerem] NUMERIC not null,
    [CFG_PosunNaDalsiPolozku] NUMERIC not null,
    [CFG_PovolitZaporneMnozstvi] NUMERIC not null,
    [CFG_KontrolaUplnosti] NUMERIC not null,
    [CFG_PovolitZmenuLokace] NUMERIC not null
);

create table [UCSTR]
(
    [STREDISKO] TEXT CONSTRAINT UCSTR_STREDISKO_PK PRIMARY KEY,
    [NAZEV] TEXT,
    [UCETNI] TEXT,
    [STRED2] TEXT,
    [CINNOST] TEXT,
    [ZAK] TEXT,
    [AKTIVNI] INTEGER not null,
    [KLIC_STA] INTEGER,
    [CASZAPSANI] TEXT,
    [STRUKT] INTEGER not null,
    [STRUKTSEZN] TEXT
);
create index IDX_UCSTR_NAZEV on [UCSTR] ([NAZEV]);

