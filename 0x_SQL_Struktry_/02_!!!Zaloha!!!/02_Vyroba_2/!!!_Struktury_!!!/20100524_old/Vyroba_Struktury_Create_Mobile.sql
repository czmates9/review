DROP TABLE CZPRO_VPH;
DROP TABLE CZPRO_VPP;
DROP TABLE CORRECTS;
DROP TABLE LOGINS;
DROP TABLE MACHINES;
DROP TABLE PRODUCTION;
DROP TABLE STATUSTYPES;
DROP TABLE USEREVENTS;

CREATE TABLE CZPRO_VPH (
	CountEntries int NOT NULL DEFAULT ('1'), --èíslo dávky, 
	SOPNUMBE nvarchar (17) NOT NULL, --èíslo výr.zakázky,
	SOPTYPE nvarchar (11) NOT NULL DEFAULT (''), --typ zakázky,
	SOPDESC nvarchar (51) NULL, -- popis zakázky,
	VNDDOCNMH nvarchar (21) NULL, -- èíslo objednatele,
	BarcodeH nvarchar (31) NOT NULL, -- èár. kód zakázky,
	LOCNCODE nvarchar (11) NULL, --lokace,
	DateProd smallint NOT NULL, --oèek. Datum výroby,
	Rez1 nvarchar(11) NOT NULL, --rezerva 1,
	Rez2 nvarchar(11) NOT NULL, --rezerva 2,
	TermID tinyint NOT NULL, --ID term. – posl. naètení dat,
	LSTMod datetime NOT NULL, --èas.razítko posl. modifi.,
	DEX_ROW_ID int IDENTITY (1, 1) NOT NULL ,
CONSTRAINT PK_CZPRO_VPH PRIMARY KEY
(
 SOPNUMBE 
)
)
;

CREATE NONCLUSTERED INDEX IX_CZPRO_VPH ON CZPRO_VPH 
(
	BarcodeH ASC
);
			


CREATE TABLE CZPRO_VPP (
	CountEntries int NOT NULL DEFAULT ('1'), -- èíslo dávky,
	SOPNUMBE nvarchar (17) NOT NULL, -- èíslo výr.zakázky,
	ITEMNMBR nvarchar (31) NOT NULL, -- ID položky,
	ITEMTYPE nvarchar (11) NOT NULL DEFAULT (''),-- typ položky,
	ITEMDESC nvarchar (51) NULL, -- popis položky,
	VNDDOCNMP nvarchar (21) NULL, -- èíslo dok. externí,
	VNDITNUM nvarchar (31) NULL, --èíslo položky externí,
	ORD int NOT NULL DEFAULT (0), --poøadí položky (operace),
	BarcodeP nvarchar (31) NOT NULL, --èárový kód položky,
	LOCNCODE nvarchar (11) NULL, -- lokace,
	QTYSHPPD numeric(19, 5) NOT NULL, -- množství,
	QTYPACK numeric(19, 5) NOT NULL, -- množství v balení,
	TIMEPREP real NOT NULL, -- pøípravný èas v minutach,
	TIMEUNIT real NOT NULL, -- jednotkový èas v minutach,
	DtProdT tinyint NOT NULL, -- sledovat dat.vyr.,
	DtProdL smallint NOT NULL, -- rozsah p. dat.výr.,
	SerNumT tinyint NOT NULL, -- sledovat SN,
	SerNumL smallint NOT NULL, -- rozsah p. SN,
	VerT tinyint NOT NULL, -- sledovat verzi,
	VerL smallint NOT NULL, -- rozsah p. verze,
	TermID tinyint NOT NULL, -- pøíznak pøevzetí,
	LSTMod datetime NOT NULL, -- èas.razítko posl. modifi.,
	DEX_ROW_ID int IDENTITY (1, 1) NOT NULL,
	QTYODVEDENO numeric(19, 5) NOT NULL DEFAULT(0), --Pocet kusu odvedenych automaticky terminaly a pripadnym rucnim odvodem ze systemu
	CNTODVEDENO	numeric(19, 5) NOT NULL DEFAULT(0), --Slouzi k informaci, zda zapocitat pripravny cas do dalsiho vystupu(>0 => nezapocitat pripravny cas)
CONSTRAINT PK_CZPRO_VPP PRIMARY KEY
(
 SOPNUMBE ,
 ITEMNMBR 
)
) 
;

CREATE NONCLUSTERED INDEX IX_CZPRO_VPP ON CZPRO_VPP
(
	BarcodeP ASC
);

 
--Vystupni data


CREATE TABLE Production(
	CountEntries int NOT NULL, -- èíslo dávky,
	SOPNUMBE nvarchar(17) NULL, -- zakázka,
	ITEMNMBR nvarchar (31) NOT NULL, -- ID položky,
	ORD int NOT NULL, --poøadí položky (operace),
	TIMEPREP real NOT NULL, -- pøípravný èas v minutach,
	TIMEUNIT real NOT NULL, -- jednotkový èas v minutach,
	TIMESTART datetime NULL, -- start èas zahajeni operace,
	TIMESTOP datetime NOT NULL, -- stop èas ukonceni operace,
	TIMECOR real NOT NULL, -- korekce èasu v minutach,
	TIMECRID int NULL, -- ID korekce èasu,
	id int IDENTITY(1,1) NOT NULL, -- ID události,
	loginid nvarchar(10) NOT NULL, -- id uživatele (smeny). uzivatel, ktery prihlasil smenu,
	machineid nvarchar(16) NOT NULL, -- id stroje,
	dateeve datetime NOT NULL, -- datum a èas,
	qty numeric(19, 5) NOT NULL, -- poèet kusù zadaný uživ.,
	qtyReal numeric(19, 5) NOT NULL, -- poèet kusù sejmuto,
	description ntext NULL, -- popis,
	BarcodeP nvarchar (31) NOT NULL, -- èárový kód položky,
	UserID  nvarchar (10) NOT NULL, -- ID prac. který provedl operaci,
	TermID tinyint NOT NULL, --ID term, ktery zaznamenal provedeni operace,
	ISOK   datetime NULL, -- èas. pøevzetí do IS,
	GUID uniqueidentifier not null, --jedinecna idetifikace zaznamu, slouzi pro update do db, v pripade vicenasobneho prenosu
CONSTRAINT PK_PRODUCTION PRIMARY KEY  
(
	id 
)
)
;

CREATE NONCLUSTERED INDEX IX_PRODUCTION ON PRODUCTION
(
	SOPNUMBE,
	ITEMNMBR
);



CREATE TABLE UserEvents(
	id int IDENTITY(1,1) NOT NULL, -- ID uživatele
	loginid nvarchar(10) NOT NULL, -- id smeny (uzivatel, ktery prihlasil smenu)
	machineid nvarchar(16) NULL, -- machineid
	dateeve datetime NOT NULL, -- datum a èas události
	statusid nvarchar(10) NOT NULL, -- status id
	UserID  nvarchar (10) NOT NULL, -- ID pracovnika, ke kteremu se udalost vaze,
	TermID tinyint NOT NULL, --ID term, ktery zaznamenal vlozeni udalosti,
	GUID uniqueidentifier not null, --jedinecna idetifikace zaznamu, slouzi pro update do db, v pripade vicenasobneho prenosu
 CONSTRAINT PK_UserEvents PRIMARY KEY  
(
	id 
)
) 
;

--Èíselníky:


CREATE TABLE Logins(
	id nvarchar(10) NOT NULL, -- ID osoby (ÈK)
	firstname nvarchar(20) NOT NULL, -- køestní jméno
	surname nvarchar(50) NOT NULL, -- pøíjmení
	psswd nvarchar(10) NOT NULL, -- heslo
 CONSTRAINT PK_Logins PRIMARY KEY  
(
	id 
)
) 
;


CREATE TABLE Machines(
	id nvarchar(16) NOT NULL, -- ID stroje
	name nvarchar(20) NOT NULL, -- jméno stroje
	description nvarchar(50) NOT NULL, -- popis
 CONSTRAINT PK_Machines PRIMARY KEY  
(
	id 
)
) 
;


CREATE TABLE StatusTypes(
	statusid nvarchar(10) NOT NULL, -- ID
	statusdesc nvarchar(100) NULL, -- popis
	--statustype nvarchar (1) NULL DEFAULT (''), -- type statusu udalosti ()
 CONSTRAINT PK_StatusTypes PRIMARY KEY  
(
	statusid 
)
) 
;


CREATE TABLE Corrects(
	id int NOT NULL, -- ID korekce
	[desc] nvarchar(10) NOT NULL, -- popis korekce
	TMFrom real NULL DEFAULT (0), -- minimalni doba trvani korekce operace(je-li NULL, pak min=0),
	TMTo real NULL, -- maximalni doba trvani korekce operace(je-li NULL, pak neni omezeno),
CONSTRAINT PK_correct PRIMARY KEY  
(
	id 
)
) 
;

