/** Pokud tabulky existuji, pak je smaze a provede nove zalozeni, data budou smazana **/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[CZPRO_VPP_View]'))
DROP VIEW [dbo].[CZPRO_VPP_View]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Corrects]') AND type in (N'U'))
DROP TABLE [dbo].[Corrects]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StatusTypes]') AND type in (N'U'))
DROP TABLE [dbo].[StatusTypes]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Machines]') AND type in (N'U'))
DROP TABLE [dbo].[Machines]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Logins]') AND type in (N'U'))
DROP TABLE [dbo].[Logins]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserEvents]') AND type in (N'U'))
DROP TABLE [dbo].[UserEvents]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Production]') AND type in (N'U'))
DROP TABLE [dbo].[Production]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CZPRO_VPP]') AND type in (N'U'))
DROP TABLE [dbo].[CZPRO_VPP]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CZPRO_VPH]') AND type in (N'U'))
DROP TABLE [dbo].[CZPRO_VPH]
GO


/****** Object:  Table [dbo].[CZPRO_VPH] ******/
CREATE TABLE [dbo].[CZPRO_VPH] (
	[CountEntries] [int] NOT NULL DEFAULT ('1'), --èíslo dávky, 
	[SOPNUMBE] [varchar] (17) NOT NULL, --èíslo výr.zakázky,
	[SOPTYPE] [varchar] (11) NOT NULL DEFAULT (''), --typ zakázky,
	[SOPDESC] [varchar] (51) NULL, -- popis zakázky,
	[VNDDOCNMH] [varchar] (21) NULL, -- èíslo objednatele,
	[BarcodeH] [varchar] (31) NOT NULL, -- èár. kód zakázky,
	[LOCNCODE] [varchar] (11) NULL, --lokace,
	[DateProd] [smallint] NOT NULL, --oèek. Datum výroby,
	[Rez1] [varchar](11) NOT NULL, --rezerva 1,
	[Rez2] [varchar](11) NOT NULL, --rezerva 2,
	[TermID] [tinyint] NOT NULL, --ID term. – posl. naètení dat,
	[LSTMod] [datetime] NOT NULL, --èas.razítko posl. modifi.,
	[DEX_ROW_ID] [int] IDENTITY (1, 1) NOT NULL ,
CONSTRAINT [PK_CZPRO_VPH] PRIMARY KEY
(
 [SOPNUMBE] ASC
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[CZPRO_VPP] ******/
CREATE TABLE [dbo].[CZPRO_VPP] (
	[CountEntries] [int] NOT NULL DEFAULT ('1'), -- èíslo dávky,
	[SOPNUMBE] [varchar] (17) NOT NULL, -- èíslo výr.zakázky,
	[ITEMNMBR] [varchar] (31) NOT NULL, -- ID položky,
	[ITEMTYPE] [varchar] (11) NOT NULL DEFAULT (''),-- typ položky,
	[ITEMDESC] [varchar] (51) NULL, -- popis položky,
	[VNDDOCNMP] [varchar] (21) NULL, -- èíslo dok. externí,
	[VNDITNUM] [varchar] (31) NULL, --èíslo položky externí,
	[ORD] [int] NOT NULL DEFAULT (0), --poøadí položky (operace),
	[BarcodeP] [varchar] (31) NOT NULL, --èárový kód položky,
	[LOCNCODE] [varchar] (11) NULL, -- lokace,
	[QTYSHPPD] [numeric](19, 5) NOT NULL, -- množství,
	[QTYDOKON] [numeric](19, 5) NOT NULL DEFAULT (0), -- množství dokoncene rucnim odvodem,
	[QTYPACK] [numeric](19, 5) NOT NULL, -- množství v balení,
	[TIMEPREP] [real] NOT NULL, -- pøípravný èas v minutach,
	[TIMEUNIT] [real] NOT NULL, -- jednotkový èas v minutach,
	[DtProdT] [tinyint] NOT NULL, -- sledovat dat.vyr.,
	[DtProdL] [smallint] NOT NULL, -- rozsah p. dat.výr.,
	[SerNumT] [tinyint] NOT NULL, -- sledovat SN,
	[SerNumL] [smallint] NOT NULL, -- rozsah p. SN,
	[VerT] [tinyint] NOT NULL, -- sledovat verzi,
	[VerL] [smallint] NOT NULL, -- rozsah p. verze,
	[TermID] [tinyint] NOT NULL, -- pøíznak pøevzetí,
	[LSTMod] [datetime] NOT NULL, -- èas.razítko posl. modifi.,
	[DEX_ROW_ID] [int] IDENTITY (1, 1) NOT NULL,
CONSTRAINT [PK_CZPRO_VPP] PRIMARY KEY
(
 [SOPNUMBE] ASC,
 [ITEMNMBR] ASC
)
) ON [PRIMARY]
GO

 
--Vystupni data

/****** Object:  Table [dbo].[Production] ******/
CREATE TABLE [dbo].[Production](
	[CountEntries] [int] NOT NULL, -- èíslo dávky,
	[SOPNUMBE] [varchar](17) NULL, -- zakázka,
	[ITEMNMBR] [varchar] (31) NOT NULL, -- ID položky,
	[ORD] [int] NOT NULL, --poøadí položky (operace),
	[TIMEPREP] [real] NOT NULL, -- pøípravný èas v minutach,
	[TIMEUNIT] [real] NOT NULL, -- jednotkový èas v minutach,
	[TIMESTART] [datetime] NULL, -- start èas zahajeni operace,
	[TIMESTOP] [datetime] NOT NULL, -- stop èas ukonceni operace,
	[TIMECOR] [real] NOT NULL, -- korekce èasu v minutach,
	[TIMECRID] [int] NULL, -- ID korekce èasu,
	[id] [int] IDENTITY(1,1) NOT NULL, -- ID události,
	[loginid] [varchar](10) NOT NULL, -- id uživatele (smeny). uzivatel, ktery prihlasil smenu,
	[machineid] [varchar](16) NOT NULL, -- id stroje,
	[dateeve] [datetime] NOT NULL, -- datum a èas,
	[qty] [numeric](19, 5) NOT NULL, -- poèet kusù zadaný uživ.,
	[qtyReal] [numeric](19, 5) NOT NULL, -- poèet kusù sejmuto,
	[description] [ntext] NULL, -- popis,
	[BarcodeP] [varchar] (31) NOT NULL, -- èárový kód položky,
	[UserID]  [varchar] (10) NOT NULL, -- ID prac. který provedl operaci,
	[TermID] [tinyint] NOT NULL, --ID term, ktery zaznamenal provedeni operace,
	[ISOK]   [datetime] NULL, -- èas. pøevzetí do IS,
	[GUID] [uniqueidentifier] not null, --jedinecna idetifikace zaznamu, slouzi pro update do db, v pripade vicenasobneho prenosu
CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserEvents] ******/
CREATE TABLE [dbo].[UserEvents](
	[id] [int] IDENTITY(1,1) NOT NULL, -- ID uživatele
	[loginid] [varchar](10) NOT NULL, -- id smeny (uzivatel, ktery prihlasil smenu)
	[machineid] [varchar](16) NULL, -- machineid
	[dateeve] [datetime] NOT NULL, -- datum a èas události
	[statusid] [varchar](10) NOT NULL, -- status id
	[UserID]  [varchar] (10) NOT NULL, -- ID pracovnika, ke kteremu se udalost vaze,
	[TermID] [tinyint] NOT NULL, --ID term, ktery zaznamenal vlozeni udalosti,
	[GUID] [uniqueidentifier] not null, --jedinecna idetifikace zaznamu, slouzi pro update do db, v pripade vicenasobneho prenosu
 CONSTRAINT [PK_UserEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
 

--Èíselníky:

/****** Object:  Table [dbo].[Logins] ******/
CREATE TABLE [dbo].[Logins](
	[id] [varchar](10) NOT NULL, -- ID osoby (ÈK)
	[firstname] [varchar](20) NOT NULL, -- køestní jméno
	[surname] [varchar](50) NOT NULL, -- pøíjmení
	[psswd] [varchar](10) NOT NULL, -- heslo
 CONSTRAINT [PK_Logins] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Machines] ******/
CREATE TABLE [dbo].[Machines](
	[id] [varchar](16) NOT NULL, -- ID stroje
	[name] [varchar](20) NOT NULL, -- jméno stroje
	[description] [varchar](50) NOT NULL, -- popis
 CONSTRAINT [PK_Machines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[StatusTypes] ******/
CREATE TABLE [dbo].[StatusTypes](
	[statusid] [varchar](10) NOT NULL, -- ID
	[statusdesc] [varchar](100) NULL, -- popis
	--[statustype] [varchar] (1) NULL DEFAULT (''), -- type statusu udalosti ()
 CONSTRAINT [PK_StatusTypes] PRIMARY KEY CLUSTERED 
(
	[statusid] ASC
)
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Corrects] ******/
CREATE TABLE [dbo].[Corrects](
	[id] [int] NOT NULL, -- ID korekce
	[desc] [varchar](10) NOT NULL, -- popis korekce
	[TMFrom] [real] NULL DEFAULT (0), -- minimalni doba trvani korekce operace(je-li NULL, pak min=0),
	[TMTo] [real] NULL, -- maximalni doba trvani korekce operace(je-li NULL, pak neni omezeno),
CONSTRAINT [PK_correct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]
GO


/****** Object:  View [dbo].[CZPRO_VPP_View]    Script Date: 06/24/2009 09:38:05 ******/
CREATE VIEW [dbo].[CZPRO_VPP_View]
AS
SELECT    
	vpp.CountEntries, 
	vpp.SOPNUMBE, 
	vpp.ITEMNMBR, 
	vpp.ITEMTYPE, 
	vpp.ITEMDESC, 
	vpp.VNDDOCNMP, 
	vpp.VNDITNUM, 
	vpp.ORD, 
	vpp.BarcodeP, 
	vpp.LOCNCODE, 
	vpp.QTYSHPPD, 
	vpp.QTYPACK, 
	vpp.TIMEPREP, 
	vpp.TIMEUNIT, 
	vpp.DtProdT, 
	vpp.DtProdL, 
	vpp.SerNumT, 
	vpp.SerNumL, 
	vpp.VerT, 
	vpp.VerL, 
	vpp.TermID, 
	vpp.LSTMod, --psum.LSTMod, 
	vpp.DEX_ROW_ID, 
	vpp.QTYDOKON + ISNULL(psum.QTYODVEDENO, 0) AS QTYODVEDENO, --Pocet kusu odvedenych automaticky terminaly a pripadnym rucnim odvodem ze systemu
	ISNULL(psum.CNTODVEDENO, 0) AS CNTODVEDENO	--Slouzi k informaci, zda zapocitat pripravny cas do dalsiho vystupu(>0 => nezapocitat pripravny cas)
FROM         
	dbo.CZPRO_VPP 
AS vpp 
LEFT OUTER JOIN 
(
	SELECT SOPNUMBE, ITEMNMBR, SUM(qty) AS QTYODVEDENO, Count(*) AS CNTODVEDENO --, MAX(dateeve) as LSTMod
	FROM   dbo.Production
	GROUP BY SOPNUMBE, ITEMNMBR
) AS psum ON psum.SOPNUMBE = vpp.SOPNUMBE AND psum.ITEMNMBR = vpp.ITEMNMBR

GO
