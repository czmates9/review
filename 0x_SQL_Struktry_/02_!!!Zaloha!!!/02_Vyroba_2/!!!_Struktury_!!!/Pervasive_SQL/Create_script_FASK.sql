-- not null a default nelze kombinovat >> ok, dulezite zajistit naplneni 
-- numeric - max 15, 5   >> nemel by byt zasadni problem
-- datetime -> timestamp >> ok 
-- ntext -> varchar(128) >> ok
-- GUID uniqueidentifier - až od verze PSQL 9.5 (v Rokospolu OK) >> zustava GUID uniqueidentifier
-- desc je rezervované slovo (nutno tedy psát v uvozovkách) >> prejmenovano na "description" (toto je pouzito i v ostatnich tabulkach)
-- CZPRO_VPP_View >> doplneny sloupce ITEMMJ a QTYPACKMJ
-- CZPRO_VPP_View, ProductionSum_View >> 21.6.2010 : upraveno, rozisren vazebni klic na (CountEntries,SOPNUMBE,ORD,ITEMNMBR,ITEMTYPE,QTYPACK)


CREATE TABLE CZPRO_VPH (
	CountEntries int DEFAULT 1, --èíslo dávky, 
	SOPNUMBE varchar (17) NOT NULL, --èíslo výr.zakázky,
	SOPTYPE varchar (11) DEFAULT '', --typ zakázky,
	SOPDESC varchar (51) NULL, -- popis zakázky,
	VNDDOCNMH varchar (21) NULL, -- èíslo objednatele,
	BarcodeH varchar (31) NOT NULL, -- èár. kód zakázky,
	LOCNCODE varchar (11) NULL, --lokace,
	DateProd smallint NOT NULL, --oèek. Datum výroby,
	Rez1 varchar(11) NOT NULL, --rezerva 1,
	Rez2 varchar(11) NOT NULL, --rezerva 2,
	TermID tinyint NOT NULL, --ID term. - posl. naètení dat,
	LSTMod timestamp NOT NULL, --èas.razítko posl. modifi.,
	DEX_ROW_ID IDENTITY ,
CONSTRAINT PK_CZPRO_VPH PRIMARY KEY
(
 SOPNUMBE, CountEntries
)
)

#

CREATE TABLE CZPRO_VPP (
	CountEntries int DEFAULT 1, -- èíslo dávky,
	SOPNUMBE varchar (17) NOT NULL, -- èíslo výr.zakázky,
	ITEMNMBR varchar (31) NOT NULL, -- ID položky,
	ITEMTYPE varchar (11) DEFAULT '',-- typ položky,
	ITEMDESC varchar (51) NULL, -- popis položky,
	ITEMMJ varchar (5) NULL, -- mìrná jednotka položky
	VNDDOCNMP varchar (21) NULL, -- èíslo dok. externí,
	VNDITNUM varchar (31) NULL, --èíslo položky externí,
	ORD int DEFAULT 0, --poøadí položky (operace),
	BarcodeP varchar (31) NOT NULL, --èárový kód položky,
	LOCNCODE varchar (11) NULL, -- lokace,
	QTYSHPPD numeric(15, 5) NOT NULL, -- množství,
	QTYDOKON numeric(15, 5) DEFAULT 0, -- množství dokoncene rucnim odvodem,
	QTYPACK numeric(15, 5) NOT NULL, -- množství v balení(pøepoètový koeficent)
	QTYPACKMJ varchar (5) NULL, --mìrná jednotka balení,
	TIMEPREP real NOT NULL, -- pøípravný èas v minutach,
	TIMEUNIT real NOT NULL, -- jednotkový èas v minutach,
	DtProdT tinyint NOT NULL, -- sledovat dat.vyr.,
	DtProdL smallint NOT NULL, -- rozsah p. dat.výr.,
	SerNumT tinyint NOT NULL, -- sledovat SN,
	SerNumL smallint NOT NULL, -- rozsah p. SN,
	VerT tinyint NOT NULL, -- sledovat verzi,
	VerL smallint NOT NULL, -- rozsah p. verze,
	TermID tinyint NOT NULL, -- pøíznak pøevzetí,
	LSTMod timestamp NOT NULL, -- èas.razítko posl. modifi.,
	DEX_ROW_ID IDENTITY NOT NULL,
CONSTRAINT PK_CZPRO_VPP PRIMARY KEY
(
 SOPNUMBE,
 ITEMNMBR,
 CountEntries
)
)

#

CREATE TABLE Production(
	CountEntries int NOT NULL, -- èíslo dávky,
	SOPNUMBE varchar(17) NULL, -- zakázka,
	ITEMNMBR varchar (31) NOT NULL, -- ID položky,
	ITEMTYPE varchar (11) DEFAULT '',-- typ položky,
	ITEMMJ varchar (5) NULL, -- mìrná jednotka položky
	ORD int NOT NULL, --poøadí položky (operace),
	TIMEPREP real NOT NULL, -- pøípravný èas v minutach,
	TIMEUNIT real NOT NULL, -- jednotkový èas v minutach,
	TIMESTART timestamp NULL, -- start èas zahajeni operace,
	TIMESTOP timestamp NOT NULL, -- stop èas ukonceni operace,
	TIMECOR real NOT NULL, -- korekce èasu v minutach,
	TIMECRID int NULL, -- ID korekce èasu,
	id IDENTITY NOT NULL, -- ID události,
	loginid varchar(10) NOT NULL, -- id uživatele (smeny). uzivatel, ktery prihlasil smenu,
	machineid varchar(16) NOT NULL, -- id stroje,
	dateeve timestamp NOT NULL, -- datum a èas,
	qty numeric(15, 5) NOT NULL, -- poèet kusù zadaný uživ.,
	qtyReal numeric(15, 5) NOT NULL, -- poèet kusù sejmuto,
	QTYPACK numeric(15, 5) NOT NULL, -- množství v balení(pøepoètový koeficent)
	QTYPACKMJ varchar (5) NULL, --mìrná jednotka balení,
	description varchar(128) NULL, -- popis,
	BarcodeP varchar (31) NOT NULL, -- èárový kód položky,
	UserID  varchar (10) NOT NULL, -- ID prac. který provedl operaci,
	TermID tinyint NOT NULL, --ID term, ktery zaznamenal provedeni operace,
	ISOK   timestamp NULL, -- èas. pøevzetí do IS,
	GUID uniqueidentifier not null, --jedinecna idetifikace zaznamu, slouzi pro update do db, v pripade vicenasobneho prenosu
CONSTRAINT PK_Events PRIMARY KEY  
(
	id 
)
)

#

CREATE TABLE UserEvents(
	id IDENTITY NOT NULL, -- ID uživatele
	loginid varchar(10) NOT NULL, -- id smeny (uzivatel, ktery prihlasil smenu)
	machineid varchar(16) NULL, -- machineid
	dateeve timestamp NOT NULL, -- datum a èas události
	statusid varchar(10) NOT NULL, -- status id
	UserID  varchar (10) NOT NULL, -- ID pracovnika, ke kteremu se udalost vaze,
	TermID tinyint NOT NULL, --ID term, ktery zaznamenal vlozeni udalosti,
	REZ1 varchar (20) NULL, --Dopòující údaj události obsluhy (napø. è.výrobního pøíkazu)
	GUID uniqueidentifier not null, --jedinecna idetifikace zaznamu, slouzi pro update do db, v pripade vicenasobneho prenosu
 CONSTRAINT PK_UserEvents PRIMARY KEY 
(
	id 
)
)

#

CREATE TABLE Logins(
	id varchar(10) NOT NULL, -- ID osoby (ÈK)
	firstname varchar(20) NOT NULL, -- køestní jméno
	surname varchar(50) NOT NULL, -- pøíjmení
	psswd varchar(10) NOT NULL, -- heslo
 CONSTRAINT PK_Logins PRIMARY KEY 
(
	id 
)
)

#

CREATE TABLE Machines(
	id varchar(16) NOT NULL, -- ID stroje
	name varchar(20) NOT NULL, -- jméno stroje
	description varchar(50) NOT NULL, -- popis
 CONSTRAINT PK_Machines PRIMARY KEY 
(
	id 
)
)

#

CREATE TABLE StatusTypes(
	statusid varchar(10) NOT NULL, -- ID
	statusdesc varchar(100) NULL, -- popis
	--statustype varchar (1) NULL DEFAULT (''), -- type statusu udalosti ()
 CONSTRAINT PK_StatusTypes PRIMARY KEY 
(
	statusid 
)
)

#

CREATE TABLE Corrects(
	id int NOT NULL, -- ID korekce
	description varchar(10) NOT NULL, -- popis korekce
	TMFrom real DEFAULT 0, -- minimalni doba trvani korekce operace(je-li NULL, pak min=0),
	TMTo real NULL, -- maximalni doba trvani korekce operace(je-li NULL, pak neni omezeno),
CONSTRAINT PK_correct PRIMARY KEY  
(
	id
)
)

#

CREATE VIEW "ProductionSum_View" AS SELECT "T1" ."CountEntries" ,"T1" ."SOPNUMBE" ,"T1" ."ORD" ,"T1" ."ITEMNMBR" ,"T1" ."ITEMTYPE" ,"T1" ."QTYPACK" ,SUM ("T1" ."qty" )"QTYODVEDENO" ,COUNT (*)"CNTODVEDENO" FROM "Production" "T1" GROUP BY "T1" ."CountEntries" ,"T1" ."SOPNUMBE" ,"T1" ."ORD" ,"T1" ."ITEMNMBR" ,"T1" ."ITEMTYPE" ,"T1" ."QTYPACK" 
#

CREATE VIEW "CZPRO_VPP_View" AS SELECT "vpp" ."CountEntries" ,"vpp" ."SOPNUMBE" ,"vpp" ."ITEMNMBR" ,"vpp" ."ITEMTYPE" ,"vpp" ."ITEMDESC" ,"vpp" ."ITEMMJ" ,"vpp" ."VNDDOCNMP" ,"vpp" ."VNDITNUM" ,"vpp" ."ORD" ,"vpp" ."BarcodeP" ,"vpp" ."LOCNCODE" ,"vpp" ."QTYSHPPD" ,"vpp" ."QTYPACK" ,"vpp" ."QTYPACKMJ" ,"vpp" ."TIMEPREP" ,"vpp" ."TIMEUNIT" ,"vpp" ."DtProdT" ,"vpp" ."DtProdL" ,"vpp" ."SerNumT" ,"vpp" ."SerNumL" ,"vpp" ."VerT" ,"vpp" ."VerL" ,"vpp" ."TermID" ,"vpp" ."LSTMod" ,"vpp" ."DEX_ROW_ID" ,"vpp" ."QTYDOKON" +"isNull" ("psum" ."QTYODVEDENO" ,0 )"QTYODVEDENO" ,"isNull" ("psum" ."CNTODVEDENO" ,0 )"CNTODVEDENO" FROM "CZPRO_VPP" "vpp" LEFT OUTER JOIN "ProductionSum_View" "psum" ON "psum" ."CountEntries" = "vpp" ."CountEntries" AND "psum" ."SOPNUMBE" = "vpp" ."SOPNUMBE" AND "psum" ."ORD" = "vpp" ."ORD" AND "psum" ."ITEMNMBR" = "vpp" ."ITEMNMBR" AND "psum" ."ITEMTYPE" = "vpp" ."ITEMTYPE" AND "psum" ."QTYPACK" = "vpp" ."QTYPACK"
